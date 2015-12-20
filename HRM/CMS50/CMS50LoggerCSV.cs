using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MGT.HRM.CMS50
{
    public class CMS50LoggerCSV : CMS50Logger
    {
        public string delimiter = ";";
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

        private StreamWriter streamWriter;

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
            streamWriter = new StreamWriter(fileName);
            StringBuilder sb = new StringBuilder();
            sb.Append("Timestamp");
            sb.Append(delimiter);
            sb.Append("Heart beat");
            sb.Append(delimiter);
            sb.Append("Heart rate");
            sb.Append(delimiter);
            sb.Append("Min heart rate");
            sb.Append(delimiter);
            sb.Append("Max heart rate");
            sb.Append(delimiter);
            sb.Append("SpO2");
            sb.Append(delimiter);
            sb.Append("Min SpO2");
            sb.Append(delimiter);
            sb.Append("Max SpO2");
            sb.Append(delimiter);
            sb.Append("Waveform");
            sb.Append(delimiter);
            streamWriter.WriteLine(sb.ToString());
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

            streamWriter.WriteLine(sb.ToString());
        }

        public override void Dispose()
        {
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
