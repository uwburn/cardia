using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace MGT.HRM.Zephyr_HxM
{
    public class ZephyrHxMLoggerUDP : ZephyrHxMNetLogger
    {
        private string delimiter = ";";
        public string Delimiter
        {
            get
            {
                return delimiter;
            }
            set
            {
                if (running)
                    throw new Exception("Delimiter cannot be changed when the logger is running");

                delimiter = value;
            }
        }

        private long packetIndex;
        private UdpClient udpClient;

        // 1 - Timestamp
        // 2 - Heart beat
        // 3 - Heart rate
        // 4 - Min heart rate
        // 5 - Max heart rate
        // 6 - RR interval

        protected override void ZephyrStart()
        {
            packetIndex = 0;
            udpClient = new UdpClient(0);
        }

        protected override void ZephyrStop()
        {
            Dispose();
        }

        protected override void ZephyrLog(ZephyrPacket zephyrPacket)
        {
            DateTime timestamp = DateTime.Now;

            if (!running)
                return;

            for (int i = 0; i < zephyrHxM.NewRRIntervals.Count; i++)
            {
                write(zephyrPacket, i, timestamp);
            }
        }

        private void write(ZephyrPacket zephyrPacket, int index, DateTime timestamp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((packetIndex++).ToString());
            sb.Append(delimiter);
            sb.Append(timestamp.ToString());
            sb.Append(delimiter);
            sb.Append(zephyrHxM.HeartBeats.ToString());
            sb.Append(delimiter);
            sb.Append(zephyrPacket.HeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(zephyrHxM.MinHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(zephyrHxM.MaxHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(zephyrPacket.rrIntervals[index].ToString());
            sb.Append(delimiter);
            sb.Append(Environment.NewLine);

            byte[] payload = Encoding.UTF8.GetBytes(sb.ToString());

            udpClient.Send(payload, payload.Length, this.ipAddress, this.destinationPort);
        }

        public override void Dispose()
        {
            udpClient.Close();
        }
    }
}
