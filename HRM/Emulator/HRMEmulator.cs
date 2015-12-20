using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MGT.HRM.Emulator
{
    public sealed class HRMEmulator : HeartRateMonitor
    {
        public override string Name { get { return "Emulator"; } }

        Timer timer;
        Random random = new Random();
        private int maxSlope = 1;

        private int MIN_BPM = 30;
        private int MAX_BPM = 240;

        public delegate void EmulatorMinBPMChangedEventHandler(object sender, int minBPM);
        public event EmulatorMinBPMChangedEventHandler EmulatorMinBPMChanged;

        public int EmulatorMinBPM
        {
            get
            {
                return MIN_BPM;
            }
            set
            {
                int bck = MIN_BPM;

                if (value > EmulatorMaxBPM)
                    EmulatorMaxBPM = value;

                MIN_BPM = value;
                if (bck != value)
                    if (EmulatorMinBPMChanged != null)
                        EmulatorMinBPMChanged(this, value);
            }
        }

        public delegate void EmulatorMaxBPMChangedEventHandler(object sender, int maxBPM);
        public event EmulatorMaxBPMChangedEventHandler EmulatorMaxBPMChanged;

        public int EmulatorMaxBPM
        {
            get
            {
                return MAX_BPM;
            }
            set
            {
                int bck = MAX_BPM;

                if (value < EmulatorMinBPM)
                    EmulatorMinBPM = value;

                MAX_BPM = value;
                if (bck != value)
                    if (EmulatorMaxBPMChanged != null)
                        EmulatorMaxBPMChanged(this, value);
            }
        }

        private int slope = 0;
        private int bpm;
        private double heartBeats = 0;

        HRMEmulatorPacket lastPacket;

        public HRMEmulator()
        {
            Running = false;
            TotalPackets = 0;
            HeartBeats = 0;
            bpm = random.Next(55, 81);
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lastPacket = new HRMEmulatorPacket(bpm);

            if (MinHeartRate == null)
            {
                MinHeartRate = (byte)bpm;
            }
            else
            {
                if (bpm < MinHeartRate)
                    MinHeartRate = (byte)bpm;
            }

            if (MaxHeartRate == null)
            {
                MaxHeartRate = (byte)bpm;
            }
            else
            {
                if (bpm > MaxHeartRate)
                    MaxHeartRate = (byte)bpm;
            }

            if (bpm >= 180)
                maxSlope = 3;
            else if (bpm >= 120)
                maxSlope = 2;
            else
                maxSlope = 1;

            int slopeVar = random.Next(-1, 2);
            slope += slopeVar;
            if (slope > maxSlope)
                slope = maxSlope;
            else if (slope < -maxSlope)
                slope = -maxSlope;
            bpm += slope;

            if (bpm > MAX_BPM)
            {
                bpm = MAX_BPM;
                slope = 0;
            }
            else if (bpm < MIN_BPM)
            {
                bpm = MIN_BPM;
                slope = 0;
            }
            
            TotalPackets++;
            heartBeats += bpm / 60D;
            HeartBeats = (int)heartBeats;

            PacketProcessedEventArgs args2 = new PacketProcessedEventArgs(lastPacket);
            base.FirePacketProcessed(args2);
        }

        public override IHRMPacket LastPacket
        {
            get
            {
                return lastPacket;
            }
            protected set
            {
                lastPacket = (HRMEmulatorPacket) value;
            }
        }

        public override int TotalPackets { get; protected set; }
        public override int CorruptedPackets { get; protected set; }

        public override int HeartBeats { get; protected set; }
        public override byte? MinHeartRate { get; protected set;}
        public override byte? MaxHeartRate { get; protected set; }

        public override int HeartRateSmoothingFactor { get; set; }

        public override double SmoothedHeartRate
        {
            get 
            {
                return 60;
            }
            protected set 
            {
                return;
            }
        }

        public override bool Running { get; protected set; }

        public override void Start()
        {
            timer.Start();
            Running = true;
        }

        public override void Stop()
        {
            timer.Stop();
            Running = false;
        }

        public override void Reset()
        {
            MinHeartRate = null;
            MaxHeartRate = null;
        }

        public override void Dispose()
        {
            timer.Dispose();
        }
    }
}
