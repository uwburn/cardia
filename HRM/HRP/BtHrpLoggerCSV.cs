using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MGT.HRM.HRP
{
    public class BtHrpLoggerCSV : BtHrpLogger
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

        protected override void BtHrpStart()
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
            streamWriter.WriteLine(sb.ToString());
        }

        protected override void BtHrpStop()
        {
            Dispose();
        }

        protected override void BtHrpLog(BtHrpPacket btHrpPacket)
        {
            DateTime timestamp = DateTime.Now;

            if (!running)
                return;

            write(btHrpPacket, timestamp);
        }

        private void write(BtHrpPacket btHrpPacket, DateTime timestamp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(timestamp.ToString());
            sb.Append(delimiter);
            sb.Append(btHrp.HeartBeats.ToString());
            sb.Append(delimiter);
            sb.Append(btHrpPacket.HeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(btHrp.MinHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(btHrp.MaxHeartRate.ToString());
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
