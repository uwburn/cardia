using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MGT.HRM.Emulator
{
    public class HRMEmulatorLoggerCSV : HRMEmulatorLogger
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

        protected override void HRMEmulatorStart()
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

        protected override void HRMEmulatorStop()
        {
            Dispose();
        }

        protected override void HRMEmulatorLog(HRMEmulatorPacket hrmEmulatorPacket)
        {
            DateTime timestamp = DateTime.Now;

            if (!running)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(timestamp.ToString());
            sb.Append(delimiter);
            sb.Append(hrmEmulator.HeartBeats.ToString());
            sb.Append(delimiter);
            sb.Append(hrmEmulatorPacket.HeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(hrmEmulator.MinHeartRate.ToString());
            sb.Append(delimiter);
            sb.Append(hrmEmulator.MaxHeartRate.ToString());
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
