using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace MGT.HRM.Emulator
{
    public class HRMEmulatorLoggerXML : HRMEmulatorFileLogger
    {
        XmlWriter xmlWriter;
        int heartbeats;
        byte? min;
        DateTime minTime;
        byte? max;
        DateTime maxTime;

        private StreamWriter streamWriter;

        protected override void HRMEmulatorStart()
        {
            streamWriter = new StreamWriter(fileName);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            xmlWriter = XmlWriter.Create(streamWriter, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Log");

            xmlWriter.WriteStartElement("StartTime");
            xmlWriter.WriteValue(DateTime.Now);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Records");
        }

        protected override void HRMEmulatorStop()
        {
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Heartbeats");
            xmlWriter.WriteValue(heartbeats);
            xmlWriter.WriteEndElement();
            
            xmlWriter.WriteStartElement("Min");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(minTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(min);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Max");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(maxTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(max);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EndTime");
            xmlWriter.WriteValue(DateTime.Now);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            Dispose();
        }

        protected override void HRMEmulatorLog(HRMEmulatorPacket hrmEmulatorPacket)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = hrmEmulator.HeartBeats;

            if (hrmEmulator.MinHeartRate < min || min == null)
            {
                min = hrmEmulator.MinHeartRate;
                minTime = timestamp;
            }
            if (hrmEmulator.MaxHeartRate > max || max == null)
            {
                max = hrmEmulator.MaxHeartRate;
                maxTime = timestamp;
            }

            if (!running)
                return;

            xmlWriter.WriteStartElement("Record");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(timestamp);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteStartElement("BPM");
            xmlWriter.WriteValue(hrmEmulatorPacket.HeartRate);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
        }

        public override void Dispose()
        {
            xmlWriter.Close();
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}
