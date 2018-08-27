using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MGT.HRM.CMS50
{
    public class CMS50LoggerXML : CMS50FileLogger
    {
        XmlWriter xmlWriter;
        int heartbeats;
        byte? minHeartRate;
        DateTime minHeartRateTime;
        byte? maxHeartRate;
        DateTime maxHeartRateTime;
        byte? minSpO2;
        DateTime minSpO2Time;
        byte? maxSpO2;
        DateTime maxSpO2Time;

        private StreamWriter streamWriter;

        protected override void CMS50Start()
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

        protected override void CMS50Stop()
        {
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Heartbeats");
            xmlWriter.WriteValue(heartbeats);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MinHeartRate");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(minHeartRateTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(minHeartRate);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MaxHeartRate");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(maxHeartRateTime);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(maxHeartRate);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MinSpO2");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(minSpO2Time);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(minSpO2);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("MaxSpO2");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(maxSpO2Time);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteValue(maxSpO2);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EndTime");
            xmlWriter.WriteValue(DateTime.Now);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();

            Dispose();
        }

        protected override void CMS50Log(CMS50Packet cms50Packet)
        {
            DateTime timestamp = DateTime.Now;

            heartbeats = cms50.HeartBeats;

            if (cms50.MinHeartRate < minHeartRate || minHeartRate == null)
            {
                minHeartRate = cms50.MinHeartRate;
                minHeartRateTime = timestamp;
            }
            if (cms50.MaxHeartRate > maxHeartRate || maxHeartRate == null)
            {
                maxHeartRate = cms50.MaxHeartRate;
                maxHeartRateTime = timestamp;
            }

            if (cms50.MinSpO2 < minSpO2 || minSpO2 == null)
            {
                minSpO2 = cms50.MinSpO2;
                minSpO2Time = timestamp;
            }
            if (cms50.MaxSpO2 > maxSpO2 || maxSpO2 == null)
            {
                maxSpO2 = cms50.MaxSpO2;
                maxSpO2Time = timestamp;
            }

            if (!running)
                return;

            xmlWriter.WriteStartElement("Record");
            xmlWriter.WriteStartAttribute("time");
            xmlWriter.WriteValue(timestamp);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteStartElement("BPM");
            xmlWriter.WriteValue(cms50Packet.HeartRate);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("SpO2");
            xmlWriter.WriteValue(cms50Packet.SpO2);
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("Waveform");
            xmlWriter.WriteValue(cms50Packet.Waveform);
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
