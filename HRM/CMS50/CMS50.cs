using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MGT.Utilities.EventHandlers;
using OpenNETCF.IO.Ports;
using log4net;

namespace MGT.HRM.CMS50
{
    public sealed class CMS50 : HeartRateMonitor
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Name { get { return "Contec CMS50"; } }

        private static TimeSpan START_TIMEOUT = new TimeSpan(0, 0, 5);
        private static TimeSpan RUN_TIMEOUT = new TimeSpan(0, 0, 10);
        private Timer timeoutTimer = new Timer(1000);
        private DateTime lastReceivedDate;

        public override bool Running { get; protected set; }
        private bool reset = false;

        private SerialPort serialPort;
        private Queue<byte> receivedData = new Queue<byte>();
        private Queue<byte> packetQueue = new Queue<byte>();
        private CMS50Packet lastPacket;
        public override IHRMPacket LastPacket 
        { 
            get
            {
                return lastPacket;
            }
            protected set
            {
                lastPacket = (CMS50Packet)value;
            }
        }
        private CMS50Packet secondLastPacket;
        public override int TotalPackets { get; protected set; }
        public override int CorruptedPackets { get; protected set; }

        // Processed data
        public override int HeartBeats { get; protected set; }

        public override byte? MinHeartRate { get; protected set; }
        public override byte? MaxHeartRate { get; protected set; }

        private byte?[] heartRateSmoothing;
        public override int HeartRateSmoothingFactor
        {
            get
            {
                return heartRateSmoothing.Length;
            }

            set
            {
                if (Running)
                    throw new Exception();

                if (value < 1)
                    value = 1;

                heartRateSmoothing = new byte?[value];
            }
        }
        public override double SmoothedHeartRate { get; protected set; }

        public byte? MinSpO2 { get; private set; }
        public byte? MaxSpO2 { get; private set; }

        private byte?[] spO2Smoothing;
        public int SpO2SmoothingFactor
        {
            get
            {
                return spO2Smoothing.Length;
            }

            set
            {
                if (Running)
                    throw new Exception();

                if (value < 1)
                    value = 1;

                spO2Smoothing = new byte?[value];
            }
        }
        public double SmoothedSpO2 { get; private set; }

        // Current packet processing
        private bool packetStarted = false;

        public CMS50()
        {
            Running = false;

            heartRateSmoothing = new byte?[1];
            spO2Smoothing = new byte?[1];

            TotalPackets = 0;
            CorruptedPackets = 0;
            HeartBeats = 0;

            timeoutTimer.Elapsed += timeoutTimer_Elapsed;
        }

        public CMS50(string serialPortName) : this()
        {
            SerialPort = serialPortName;
        }

        public delegate void SerialPortChangedEventHandler(object sender, string serialPort);
        public event SerialPortChangedEventHandler SerialPortChanged;

        public string SerialPort
        {
            get
            {
                if (serialPort != null)
                    return serialPort.PortName;
                else
                    return null;
            }
            set
            {
                string backup = SerialPort;

                serialPort = new SerialPort(value);
                serialPort.BaudRate = 19200;
                serialPort.Parity = Parity.Odd;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.ReadTimeout = 15;

                serialPort.ReceivedEvent += serialPort_ReceivedEvent;

                if (backup != value)
                    if (SerialPortChanged != null)
                        SerialPortChanged(this, value);
            }
        }

        public override void Start()
        {
            if (!Running)
            {
#if DEBUG
                logger.Debug("Starting CMS50");
#endif

                try
                {
                    lastReceivedDate = DateTime.Now;
                    serialPort.Open();
                    serialPort.Write(new byte[] { 0 }, 0, 1);
                }
                catch(Exception e)
                {
#if DEBUG
                    logger.Warn("Error opening serial port or sending CMS50 init message", e);
#endif

                    FireTimeout("CMS50 not transmitting on serial port " + serialPort.PortName);
                    //throw new Exception("CMS50 not transmitting on serial port " + serialPort.PortName);
                }
                timeoutTimer.Start();
                Running = true;
            }
        }

        void timeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeout;
            if (Running)
                timeout = RUN_TIMEOUT;
            else
                timeout = START_TIMEOUT;

            TimeSpan diff = DateTime.Now - lastReceivedDate;
            if (diff > timeout)
            {
#if DEBUG
                if (Running)
                    logger.Debug("Communication timeout elapsed");
                else
                    logger.Debug("Start timeout elapsed");
#endif

                timeoutTimer.Stop();
                Stop();
                FireTimeout($"CMS50 not transmitting on serial port ${serialPort.PortName}");
            }
        }

        public override void Stop()
        {
            if (Running)
            {
#if DEBUG
                logger.Debug("Stopping CMS50");
#endif

                Running = false;
                serialPort.Close();
                DoReset();
            }

            timeoutTimer.Stop();
        }

        public override void Reset()
        {
#if DEBUG
            logger.Debug("Resetting CMS50");
#endif

            reset = true;
        }

        private void DoReset()
        {
#if DEBUG
            logger.Debug("Resetting counters");
#endif

            TotalPackets = 0;
            CorruptedPackets = 0;
            HeartBeats = 0;
            MinHeartRate = null;
            MaxHeartRate = null;

            for (int i = 0; i < heartRateSmoothing.Length; i++)
            {
                heartRateSmoothing[i] = null;
            }
            SmoothedHeartRate = 0;

            reset = false;
        }

        void serialPort_ReceivedEvent(object sender, SerialReceivedEventArgs e)
        {
#if DEBUG
            logger.Debug("Serial port data received");
#endif

            byte[] bytes = new byte[serialPort.BytesToRead];

            serialPort.Read(bytes, 0, bytes.Length);

#if DEBUG
            logger.Debug($"Equeuing data = {bytes}");
#endif

            foreach (byte b in bytes)
            {
                receivedData.Enqueue(b);
            }

            ProcessData();
        }

        private void ResetPacketQueue()
        {
#if DEBUG
            logger.Debug("Resetting packet queue");
#endif

            packetQueue.Clear();
            packetStarted = false;
        }

        private void StartPacketQueue()
        {
#if DEBUG
            logger.Debug("Starting packet queue");
#endif

            packetQueue.Enqueue(receivedData.Dequeue());
            packetStarted = true;
            TotalPackets++;
        }

        private void BuildPacket()
        {
#if DEBUG
            logger.Debug("Building packet");
#endif

            packetQueue.Enqueue(receivedData.Dequeue());
        }

        private void ClosePacketQueue()
        {
#if DEBUG
            logger.Debug("Closing packet queue");
#endif

            packetQueue.Enqueue(receivedData.Dequeue());

            try
            {
                CMS50Packet cms50Packet = new CMS50Packet(packetQueue.ToArray());
                secondLastPacket = lastPacket;
                lastPacket = cms50Packet;

#if DEBUG
                logger.Debug($"Constructed CMS50 packet = {cms50Packet}");
#endif
            }
            catch (CorruptedPacketExcpetion e)
            {
#if DEBUG
                logger.Debug("Packet construction failed, corrupted packet", e);
#endif

                CorruptedPackets++;
            }

            ProcessPackets();

            ResetPacketQueue();
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private void ProcessData()
        {
#if DEBUG
            logger.Debug("Processing data");
#endif

            if (reset)
                DoReset();

            while (receivedData.Count > 0)
            {
                if (packetStarted == false && IsBitSet(receivedData.Peek(), 7))
                    StartPacketQueue();
                else if (packetStarted == true && !IsBitSet(receivedData.Peek(), 7) && packetQueue.Count < 4)
                    BuildPacket();
                else if (packetStarted == true && !IsBitSet(receivedData.Peek(), 7) && packetQueue.Count == 4)
                    ClosePacketQueue();
                else
                {
                    receivedData.Dequeue();
                    ResetPacketQueue();

                    CorruptedPackets++;
                }
            }
        }

        private void ProcessPackets()
        {
#if DEBUG
            logger.Debug("Processing packets");
#endif

            // Smoothed values computation
            if (heartRateSmoothing[0] == null)
            {
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    heartRateSmoothing[i] = lastPacket.PulseRate;
                }

                SmoothedHeartRate = lastPacket.PulseRate;
            }
            else
            {
                byte?[] shiftedArray = new byte?[heartRateSmoothing.Length];
                Array.Copy(heartRateSmoothing, 0, shiftedArray, 1, heartRateSmoothing.Length - 1);
                heartRateSmoothing = shiftedArray;
                heartRateSmoothing[0] = lastPacket.PulseRate;

                double d = 0;
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    d += heartRateSmoothing[i] ?? 0;
                }
                SmoothedHeartRate = d / heartRateSmoothing.Length;
            }

            if (spO2Smoothing[0] == null)
            {
                for (int i = 0; i < spO2Smoothing.Length; i++)
                {
                    spO2Smoothing[i] = lastPacket.SpO2;
                }

                SmoothedSpO2 = lastPacket.SpO2;
            }
            else
            {
                byte?[] shiftedArray = new byte?[spO2Smoothing.Length];
                Array.Copy(spO2Smoothing, 0, shiftedArray, 1, spO2Smoothing.Length - 1);
                spO2Smoothing = shiftedArray;
                spO2Smoothing[0] = lastPacket.SpO2;

                double d = 0;
                for (int i = 0; i < spO2Smoothing.Length; i++)
                {
                    d += spO2Smoothing[i] ?? 0;
                }
                SmoothedSpO2 = d / spO2Smoothing.Length;
            }

            // Computation across multiple packets
            if (MinHeartRate == null)
                MinHeartRate = lastPacket.PulseRate;
            else if (lastPacket.PulseRate < MinHeartRate)
                MinHeartRate = lastPacket.PulseRate;

            if (MaxHeartRate == null)
                MaxHeartRate = lastPacket.PulseRate;
            else if (lastPacket.PulseRate > MaxHeartRate)
                MaxHeartRate = lastPacket.PulseRate;

            if (MinSpO2 == null)
                MinSpO2 = lastPacket.SpO2;
            else if (lastPacket.SpO2 < MinSpO2)
                MinSpO2 = lastPacket.SpO2;

            if (MaxSpO2 == null)
                MaxSpO2 = lastPacket.SpO2;
            else if (lastPacket.SpO2 > MaxSpO2)
                MaxSpO2 = lastPacket.SpO2;

            if (secondLastPacket == null)
            {
                HeartBeats = 1;
                return;
            }

            if (lastPacket.Beep)
                HeartBeats++;

            lastReceivedDate = DateTime.Now;

#if DEBUG
            logger.Debug($"Firing PacketProcessed event, packet = {LastPacket}");
#endif
            PacketProcessedEventArgs args = new PacketProcessedEventArgs(LastPacket);
            base.FirePacketProcessed(args);
        }

        public override void Dispose()
        {
            if (serialPort == null)
                return;

            if (serialPort.IsOpen)
                serialPort.Close();

            serialPort.Dispose();
        }
    }
}
