using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MGT.HRM.Zephyr_HxM
{
    public class ZephyrHxMLoggerCSV : ZephyrHxMLogger
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
        // 6 - RR interval

        protected override void ZephyrStart()
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
            sb.Append("RR interval");
            sb.Append(delimiter);
            streamWriter.WriteLine(sb.ToString());
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

            streamWriter.WriteLine(sb.ToString());
        }

        public override void Dispose()
        {
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
