using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MGT.HRM.HRP
{
    public class BtHrpLoggerXML : BtHrpFileLogger
    {
        XmlWriter xmlWriter;
        int heartbeats;
        byte? min;
        DateTime minTime;
        byte? max;
        DateTime maxTime;

        private StreamWriter streamWriter;

        protected override void BtHrpStart()
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

        protected override void BtHrpStop()
        {
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Heartbeats");
            xmlWriter.WriteValue(heartbeats);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Min");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(minTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(min ?? 0);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Max");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(maxTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(max ?? 0);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EndTime");
            xmlWriter.WriteValue(DateTime.Now);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            Dispose();
        }

        protected override void BtHrpLog(BtHrpPacket btHrpPacketPacket)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = btHrp.HeartBeats;

            if (btHrp.MinHeartRate < min || min == null)
            {
                min = btHrp.MinHeartRate;
                minTime = timestamp;
            }
            if (btHrp.MaxHeartRate > max || max == null)
            {
                max = btHrp.MaxHeartRate;
                maxTime = timestamp;
            }

            if (!running)
                return;

            write(btHrpPacketPacket, timestamp);
        }

        private void write(BtHrpPacket btHrpPacketPacket, DateTime timestamp)
        {
            xmlWriter.WriteStartElement("Record");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(timestamp);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteStartElement("BPM");
            xmlWriter.WriteValue(btHrpPacketPacket.HeartRate);
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
