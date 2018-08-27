using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace MGT.HRM.CMS50
{
    public class CMS50LoggerUDP : CMS50NetLogger
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
        // 6 - SpO2
        // 7 - Min SpO2
        // 8 - Max SpO2
        // 9 - Waveform

        protected override void CMS50Start()
        {
            packetIndex = 0;
            udpClient = new UdpClient(0);
        }

        protected override void CMS50Stop()
        {
            Dispose();
        }

        protected override void CMS50Log(CMS50Packet cms50Packet)
        {
            DateTime timestamp = DateTime.Now;

            if (!running)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append((packetIndex++).ToString());
            sb.Append(delimiter);
            sb.Append(timestamp.ToString());
            sb.Append(delimiter);
            sb.Append(cms50.HeartBeats.ToString());
            sb.Append(delimiter);
            sb.Append(cms50Packet.HeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(cms50.MinHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(cms50.MaxHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(cms50Packet.SpO2.ToString());
            sb.Append(delimiter);
            sb.Append(cms50.MinSpO2.ToString());
            sb.Append(delimiter);
            sb.Append(cms50.MaxSpO2.ToString());
            sb.Append(delimiter);
            sb.Append(cms50Packet.Waveform.ToString());
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
