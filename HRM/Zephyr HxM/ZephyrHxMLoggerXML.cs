using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MGT.HRM.Zephyr_HxM
{
    public class ZephyrHxMLoggerXML : ZephyrHxMFileLogger
    {
        XmlWriter xmlWriter;
        int heartbeats;
        byte? min;
        DateTime minTime;
        byte? max;
        DateTime maxTime;

        private StreamWriter streamWriter;

        protected override void ZephyrStart()
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

        protected override void ZephyrStop()
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

        protected override void ZephyrLog(ZephyrPacket zephyrPacket)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = zephyrHxM.HeartBeats;

            if (zephyrHxM.MinHeartRate < min || min == null)
            {
                min = zephyrHxM.MinHeartRate;
                minTime = timestamp;
            }
            if (zephyrHxM.MaxHeartRate > max || max == null)
            {
                max = zephyrHxM.MaxHeartRate;
                maxTime = timestamp;
            }

            if (!running)
                return;

            for (int i = 0; i < zephyrHxM.NewRRIntervals.Count; i++)
            {
                write(zephyrPacket, i, timestamp);
            }
        }

        private void write(ZephyrPacket zephyrPacket, int index, DateTime timestamp)
        {
            xmlWriter.WriteStartElement("Record");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(timestamp);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteStartElement("BPM");
            xmlWriter.WriteValue(zephyrPacket.HeartRate);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("RRInterval");
            xmlWriter.WriteValue(zephyrPacket.rrIntervals[index]);
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
