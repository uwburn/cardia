using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using MGT.HRM;
using MGT.Utilities.Network;

namespace MGT.Cardia
{
    [XmlRoot("Cardia")]
    public class Configuration
    {
        public class LogConfiguration
        {
            public LogFormat Format = LogFormat.CSV;
        }

        public class NetworkConfiguration
        {
            public NetworkRelayMode Mode = NetworkRelayMode.Server;
            public string Nickname = "Cardia";
            public int Port = 60009;
            public string Address = "";
        }

        public class ZephyrHxMConfiguration
        {
            public string ComPort = null;
        }

        public class CMS50Configuration
        {
            public string ComPort = null;
        }

        public class BtHrpConfiguration
        {
            public string DeviceId = null;
        }

        public class HRMEmulatorConfiguration
        {
            public int Min = 30;
            public int Max = 240;
        }

        public class DeviceConfiguration 
        {
            public enum DeviceType { HRMEmulator, ZephyrHxM, CMS50, BtHrp }

            public DeviceType Type = DeviceType.ZephyrHxM;
            public ZephyrHxMConfiguration ZephyrHxM = new ZephyrHxMConfiguration();
            public CMS50Configuration CMS50 = new CMS50Configuration();
            public BtHrpConfiguration BtHrp = new BtHrpConfiguration();
            public HRMEmulatorConfiguration HRMEmulator = new HRMEmulatorConfiguration();
        }

        [XmlIgnore]
        private static XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));

        public static Configuration LoadFromFile(string fileName)
        {
            using (TextReader reader = new StreamReader(fileName))
            {
                return (Configuration)xmlSerializer.Deserialize(reader);
            }
        }

        public void SaveToFile(string fileName)
        {
            using (TextWriter writer = new StreamWriter(fileName))
            {
                xmlSerializer.Serialize(writer, this);
            }
        }

        public int WindowWidth = 608;
        public Point? WindowLocation = null;
        public int ChartTime = 5;
        public int Color = 0;
        public int Volume = 5;
        public bool PlayBeat = true;
        public bool PlayAlarm = true;
        public bool Alarm = false;
        public int AlarmLowThreshold = 40;
        public int AlarmHighThreshold = 180;
        public bool AlarmDefuse = true;
        public int AlarmDefuseTime = 10;
        public DeviceConfiguration Device = new DeviceConfiguration();
        public LogConfiguration Log = new LogConfiguration();
        public NetworkConfiguration Network = new NetworkConfiguration();
    }
}
