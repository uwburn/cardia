using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.CRC;

namespace MGT.HRM.Zephyr_HxM
{
    public class ZephyrPacket : IHRMPacket
    {
        private static CRC8 crc8 = new CRC8(0x8C);

        public const byte Stx = 0x2;
        public const byte MsgId = 0x26;
        public const byte Etx = 0x3;
        public const ushort MaxHeartBeatNumber = 255;
        public const ushort MaxTimestamp = 65535;
        public const ushort MaxDistance = 4095;
        public const ushort MaxSpeed = 4095;
        public const ushort MaxStrides = 127;

        public readonly byte dlc;
        public readonly ushort firmwareId;
        public readonly ushort firmwareVersion;
        public readonly ushort hardwareId;
        public readonly ushort hardwareVersion;
        public readonly byte batteryChargeIndicator;
        public readonly byte heartRate;
        public readonly byte heartBeatNumber;
        public readonly ushort[] hearBeatTimestamps;
        public readonly ushort distance;
        public readonly ushort instantaneousSpeed;
        public readonly byte strides;
        public readonly byte crc;

        public readonly int[] rrIntervals;

        public ZephyrPacket(byte[] bytes)
        {
            if (bytes.Length != 60)
                throw new CorruptedPacketExcpetion("Packet corrupted, wrong length. Expected 60 bytes, found " + bytes.Length);

            dlc = bytes[2];
            firmwareId = BitConverter.ToUInt16(bytes, 3);
            firmwareVersion = BitConverter.ToUInt16(bytes, 5);
            hardwareId = BitConverter.ToUInt16(bytes, 7);
            hardwareVersion = BitConverter.ToUInt16(bytes, 9);
            batteryChargeIndicator = bytes[11];
            heartRate = bytes[12];
            heartBeatNumber = bytes[13];
            hearBeatTimestamps = new ushort[15];
            for (int i = 0; i < 15; i++)
            {
                hearBeatTimestamps[i] = BitConverter.ToUInt16(bytes, (i * 2) + 14);
            }
            distance = bytes[50];
            instantaneousSpeed = bytes[52];
            strides = bytes[54];
            crc = bytes[58];
            byte computedCrc = crc8.calculate(bytes, 3, dlc);

            if (crc != computedCrc)
                throw new CorruptedPacketExcpetion("Wrong CRC");

            rrIntervals = new int[14];
            for (int i = 0; i < rrIntervals.Length; i++)
            {
                int rrInterval;
                if (hearBeatTimestamps[i] > hearBeatTimestamps[i + 1])
                    rrInterval = hearBeatTimestamps[i] - hearBeatTimestamps[i + 1];
                else
                    rrInterval = MaxTimestamp - hearBeatTimestamps[i + 1] + hearBeatTimestamps[i];
                rrIntervals[i] = rrInterval;
            }
        }

        public int HeartRate
        {
            get { return heartRate; }
        }

        public override string ToString()
        {
            return base.ToString() + "[ HeartRate = " + HeartRate + " ]";
        }

    }
}
