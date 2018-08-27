using MGT.HRM;
using MGT.HRM.CMS50;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGT.Cardia.Configuration;

namespace MGT.Cardia
{
    public class CMS50Bundle : Bundle
    {
        private readonly CMS50 cms50 = new CMS50();
        private readonly CMS50Frm cms50Frm;
        private readonly CMS50LoggerCSV csvLogger = new CMS50LoggerCSV();
        private readonly CMS50LoggerXLSX xlsxLogger = new CMS50LoggerXLSX();
        private readonly CMS50LoggerXML xmlLogger = new CMS50LoggerXML();
        private readonly CMS50LoggerUDP udpLogger = new CMS50LoggerUDP();

        public CMS50Bundle()
        {
            string[] serialPortNames = SerialPort.GetPortNames();
            Array.Sort(serialPortNames, StringComparer.InvariantCulture);

            SerialPorts = new List<string>();
            foreach (string serialPort in serialPortNames)
                SerialPorts.Add(serialPort);

            this.cms50Frm = new CMS50Frm(this);
        }

        public override HeartRateMonitor Device => cms50;
        public override HRMDeviceFrm DeviceControlForm => cms50Frm;
        public override IHRMFileLogger CSVLogger => csvLogger;
        public override IHRMFileLogger XLSXLogger => xlsxLogger;
        public override IHRMFileLogger XMLLogger => xmlLogger;
        public override IHRMNetLogger UDPLogger => udpLogger;
        public override DeviceConfiguration.DeviceType ConfigEnumerator => DeviceConfiguration.DeviceType.CMS50;

        public CMS50 CMS50 => cms50;
        public List<string> SerialPorts { get; private set; }

        public override void InitDevice()
        {
            
        }

        public override void InitControlForm()
        {
            if (SerialPorts.Count == 0)
            {
                throw new Exception("No serial port found");
            }
        }

        public override void LoadConfig(Configuration.DeviceConfiguration deviceConfiguration, LogConfiguration logConfiguration)
        {
            if (SerialPorts.Count > 0)
            {
                if (deviceConfiguration.CMS50.ComPort != null)
                {
                    foreach (string serialPort in SerialPorts)
                    {
                        if (serialPort == deviceConfiguration.CMS50.ComPort)
                        {
                            cms50.SerialPort = serialPort;
                            break;
                        }
                    }
                }
                else
                {
                    cms50.SerialPort = SerialPorts[0];
                }
            }

            udpLogger.Address = logConfiguration.Address;
            udpLogger.Port = logConfiguration.Port;
        }

        public override void SaveConfig(DeviceConfiguration deviceConfiguration)
        {
            deviceConfiguration.Type = Configuration.DeviceConfiguration.DeviceType.CMS50;
            deviceConfiguration.CMS50.ComPort = cms50.SerialPort;
        }
    }
}
