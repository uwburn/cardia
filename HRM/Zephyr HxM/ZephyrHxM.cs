using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM.Zephyr_HxM
{
    public sealed class ZephyrHxM : HeartRateMonitor
    {
        public override string Name { get { return "Zephyr HxM"; } }

        private static TimeSpan START_TIMEOUT = new TimeSpan(0, 0, 10);
        private static TimeSpan RUN_TIMEOUT = new TimeSpan(0, 0, 30);
        private Timer timeoutTimer = new Timer(1000);
        private DateTime lastReceivedDate;

        public override bool Running { get; protected set; }
        private bool reset = false;

        private SerialPort serialPort;
        private Queue<byte> receivedData = new Queue<byte>();
        private Queue<byte> packetQueue = new Queue<byte>();
        private ZephyrPacket lastPacket;
        public override IHRMPacket LastPacket 
        { 
            get
            {
                return lastPacket;
            }
            protected set
            {
                lastPacket = (ZephyrPacket)value;
            }
        }
        private ZephyrPacket secondLastPacket;
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
        
        private byte?[] batteryLevelSmoothing;
        public int BatteryLevelSmoothingFactor
        {
            get
            {
                return batteryLevelSmoothing.Length;
            }

            set
            {
                if (Running)
                    throw new Exception();

                if (value < 1)
                    value = 1;

                batteryLevelSmoothing = new byte?[value];
            }
        }
        public double SmoothedBatteryLevel { get; private set; }

        public List<int> NewRRIntervals { get; private set; }

        // Current packet processing
        private bool packetStarted = false;
        private bool msgIdRecognized = false;
        private int packetLength = 3;

        public ZephyrHxM()
        {
            Running = false;

            heartRateSmoothing = new byte?[1];
            batteryLevelSmoothing = new byte?[1];

            TotalPackets = 0;
            CorruptedPackets = 0;
            HeartBeats = 0;

            timeoutTimer.Elapsed += timeoutTimer_Elapsed;
        }

        public ZephyrHxM(string serialPortName) : this()
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
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.ReadTimeout = 15;

                serialPort.DataReceived += DataReceivedHandler;

                if (backup != value)
                    if (SerialPortChanged != null)
                        SerialPortChanged(this, value);
            }
        }

        public override void Start()
        {
            if (!Running)
            {
                try
                {
                    lastReceivedDate = DateTime.Now;
                    serialPort.Open();
                }
                catch
                {
                    FireTimeout("Zephyr HxM not transmitting on serial port " + serialPort.PortName);
                    //throw new Exception("Zephyr HxM not transmitting on serial port " + serialPort.PortName);
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
                Stop();
                FireTimeout("Zephyr HxM not transmitting on serial port " + serialPort.PortName);
            }
        }

        public override void Stop()
        {
            if (Running)
            {
                Running = false;
                serialPort.Close();
                timeoutTimer.Stop();
                DoReset();
            }
        }

        public override void Reset()
        {
            reset = true;
        }

        private void DoReset()
        {
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

            for (int i = 0; i < batteryLevelSmoothing.Length; i++)
            {
                batteryLevelSmoothing[i] = null;
            }
            SmoothedBatteryLevel = 0;
            NewRRIntervals = null;

            reset = false;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] bytes = new byte[serialPort.BytesToRead];

            serialPort.Read(bytes, 0, bytes.Length);

            foreach (byte b in bytes)
            {
                receivedData.Enqueue(b);
            }

            ProcessData();
        }

        private void ResetPacketQueue()
        {
            packetQueue.Clear();
            packetStarted = false;
            msgIdRecognized = false;
            packetLength = 3;
        }

        private void StartPacketQueue()
        {
            packetQueue.Enqueue(receivedData.Dequeue());
            packetStarted = true;
            TotalPackets++;
        }

        private void RecognizeMessageId()
        {
            packetQueue.Enqueue(receivedData.Dequeue());
            msgIdRecognized = true;
        }

        private void BuildPacket()
        {
            byte b = receivedData.Dequeue();
            packetQueue.Enqueue(b);

            if (packetQueue.Count == 3)
                packetLength = b + 4;
        }

        private void ClosePacketQueue()
        {
            packetQueue.Enqueue(receivedData.Dequeue());

            try
            {
                ZephyrPacket zephyrPacket = new ZephyrPacket(packetQueue.ToArray());
                secondLastPacket = lastPacket;
                lastPacket = zephyrPacket;
            }
            catch (CorruptedPacketExcpetion)
            {
                CorruptedPackets++;
            }

            ProcessPackets();

            ResetPacketQueue();
        }

        private void ProcessData()
        {
            if (reset)
                DoReset();

            while (receivedData.Count > 0)
            {
                if (packetStarted == false && receivedData.Peek() == ZephyrPacket.Stx)
                    StartPacketQueue();
                else if (packetStarted == true && receivedData.Peek() == ZephyrPacket.MsgId)
                    RecognizeMessageId();
                else if (msgIdRecognized == true && packetQueue.Count == packetLength && receivedData.Peek() == ZephyrPacket.Etx)
                    ClosePacketQueue();
                else if (msgIdRecognized)
                    BuildPacket();
                else
                    receivedData.Dequeue();

                if (packetQueue.Count > (packetLength + 1))
                {
                    ResetPacketQueue();

                    CorruptedPackets++;
                }
            }
        }

        private void ProcessPackets()
        {
            // Smoothed values computation
            if (heartRateSmoothing[0] == null)
            {
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    heartRateSmoothing[i] = lastPacket.heartRate;
                }

                SmoothedHeartRate = lastPacket.heartRate;
            }
            else
            {
                byte?[] shiftedArray = new byte?[heartRateSmoothing.Length];
                Array.Copy(heartRateSmoothing, 0, shiftedArray, 1, heartRateSmoothing.Length - 1);
                heartRateSmoothing = shiftedArray;
                heartRateSmoothing[0] = lastPacket.heartRate;

                double d = 0;
                for (int i = 0; i < heartRateSmoothing.Length; i++)
                {
                    d += heartRateSmoothing[i] ?? 0;
                }
                SmoothedHeartRate = d / heartRateSmoothing.Length;
            }

            if (batteryLevelSmoothing[0] == null)
            {
                for (int i = 0; i < batteryLevelSmoothing.Length; i++)
                {
                    batteryLevelSmoothing[i] = lastPacket.batteryChargeIndicator;
                }

                SmoothedBatteryLevel = lastPacket.batteryChargeIndicator;
            }
            else
            {
                byte?[] shiftedArray = new byte?[batteryLevelSmoothing.Length];
                Array.Copy(batteryLevelSmoothing, 0, shiftedArray, 1, batteryLevelSmoothing.Length - 1);
                batteryLevelSmoothing = shiftedArray;
                batteryLevelSmoothing[0] = lastPacket.batteryChargeIndicator;

                double d = 0;
                for (int i = 0; i < batteryLevelSmoothing.Length; i++)
                {
                    d += batteryLevelSmoothing[i] ?? 0;
                }
                SmoothedBatteryLevel = d / batteryLevelSmoothing.Length;
            }

            // Computation across multiple packets
            if (MinHeartRate == null && lastPacket.heartRate > 30)
                MinHeartRate = lastPacket.heartRate;
            else if (lastPacket.heartRate < MinHeartRate && lastPacket.heartRate > 30)
                MinHeartRate = lastPacket.heartRate;

            if (MaxHeartRate == null && lastPacket.heartRate < 240)
                MaxHeartRate = lastPacket.heartRate;
            else if (lastPacket.heartRate > MaxHeartRate && lastPacket.heartRate < 240)
                MaxHeartRate = lastPacket.heartRate;

            if (secondLastPacket == null)
            {
                HeartBeats = 1;
                return;
            }

            int newHeartBeats;
            if (lastPacket.heartBeatNumber == 0 && secondLastPacket.heartBeatNumber == 0)
                newHeartBeats = 0;
            else if (lastPacket.heartBeatNumber > secondLastPacket.heartBeatNumber)
                newHeartBeats = lastPacket.heartBeatNumber - secondLastPacket.heartBeatNumber;
            else
                newHeartBeats = ZephyrPacket.MaxHeartBeatNumber - secondLastPacket.heartBeatNumber + lastPacket.heartBeatNumber;

            HeartBeats += newHeartBeats;

            NewRRIntervals = new List<int>();
            if (newHeartBeats == 1)
            {
                int rrInterval;
                if (lastPacket.hearBeatTimestamps[0] > lastPacket.hearBeatTimestamps[1])
                    rrInterval = lastPacket.hearBeatTimestamps[0] - lastPacket.hearBeatTimestamps[1];
                else
                    rrInterval = ZephyrPacket.MaxTimestamp - lastPacket.hearBeatTimestamps[1] + lastPacket.hearBeatTimestamps[0];
                NewRRIntervals.Add(rrInterval);
            }
            else if (newHeartBeats > 1)
            {
                int min = Math.Min(newHeartBeats, lastPacket.hearBeatTimestamps.Length - 1);
                for (int i = 0; i < min; i++)
                {
                    int rrInterval;
                    if (lastPacket.hearBeatTimestamps[i] > lastPacket.hearBeatTimestamps[i + 1])
                        rrInterval = lastPacket.hearBeatTimestamps[i] - lastPacket.hearBeatTimestamps[i + 1];
                    else
                        rrInterval = ZephyrPacket.MaxTimestamp - lastPacket.hearBeatTimestamps[i + 1] + lastPacket.hearBeatTimestamps[i];
                    NewRRIntervals.Add(rrInterval);
                }
            }

            lastReceivedDate = DateTime.Now;

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
