using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.CRC;

namespace MGT.HRM.CMS50
{
    public class CMS50Packet : IHRMPacket
    {
        public const ushort MaxHeartBeatNumber = 255;

        public readonly byte SignalStrength;
        public readonly bool SearchingTooLong;
        public readonly bool SpO2Drop;
        public readonly bool Beep;
        public readonly byte Waveform;
        public readonly byte BarGraph;
        public readonly bool ProbeError;
        public readonly bool Searching;
        public readonly byte PulseRate;
        public readonly byte SpO2;

        private byte? derivatedBPM;

        public CMS50Packet(byte[] bytes)
        {
            if (bytes.Length != 5)
                throw new CorruptedPacketExcpetion("Packet corrupted, wrong length. Expected 5 bytes, found " + bytes.Length);

            SignalStrength = (byte)(bytes[0] & 7);

            int searchingTooLong = bytes[0] >> 3;
            searchingTooLong &= 1;
            SearchingTooLong = searchingTooLong == 1;

            int spo2Drop = bytes[0] >> 4;
            spo2Drop &= 1;
            SpO2Drop = spo2Drop == 1;

            int beep = bytes[0] >> 5;
            beep &= 1;
            Beep = beep == 1;

            Waveform = (byte)(bytes[1] & 127);

            BarGraph = (byte)(bytes[2] & 7);

            int probeError = bytes[2] >> 3;
            probeError &= 1;
            ProbeError = probeError == 1;

            int searching = bytes[2] >> 4;
            searching &= 1;
            Searching = searching == 1;

            PulseRate = (byte)((bytes[2] & 64) + (bytes[3] & 127));

            SpO2 = (byte)(bytes[4] & 127);
        }

        public int HeartRate
        {
            get { return derivatedBPM ?? PulseRate; }
        }

        public byte? DerivatedBPM
        {
            get { return derivatedBPM; }
            set
            {
                if (derivatedBPM != null)
                    throw new Exception("Derivated BPM value already setted");

                derivatedBPM = value;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "[ HeartRate = " + HeartRate + " ]";
        }
    }
}
