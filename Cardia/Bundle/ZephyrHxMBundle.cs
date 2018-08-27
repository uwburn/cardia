using MGT.HRM;
using MGT.HRM.Zephyr_HxM;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGT.Cardia.Configuration;

namespace MGT.Cardia
{
    public class ZephyrHxMBundle : Bundle
    {
        private readonly ZephyrHxM zephyrHxM = new ZephyrHxM();
        private readonly ZephyrHxMFrm zephyrHxMFrm;
        private readonly ZephyrHxMLoggerCSV csvLogger = new ZephyrHxMLoggerCSV();
        private readonly ZephyrHxMLoggerXLSX xlsxLogger = new ZephyrHxMLoggerXLSX();
        private readonly ZephyrHxMLoggerXML xmlLogger = new ZephyrHxMLoggerXML();
        private readonly ZephyrHxMLoggerUDP udpLogger = new ZephyrHxMLoggerUDP();

        public ZephyrHxMBundle()
        {
            string[] serialPortNames = SerialPort.GetPortNames();
            Array.Sort(serialPortNames, StringComparer.InvariantCulture);

            SerialPorts = new List<string>();
            foreach (string serialPort in serialPortNames)
                SerialPorts.Add(serialPort);

            this.zephyrHxMFrm = new ZephyrHxMFrm(this);
        }

        public override HeartRateMonitor Device => zephyrHxM;
        public override HRMDeviceFrm DeviceControlForm => zephyrHxMFrm;
        public override IHRMFileLogger CSVLogger => csvLogger;
        public override IHRMFileLogger XLSXLogger => xlsxLogger;
        public override IHRMFileLogger XMLLogger => xmlLogger;
        public override IHRMNetLogger UDPLogger => udpLogger;
        public override DeviceConfiguration.DeviceType ConfigEnumerator => DeviceConfiguration.DeviceType.ZephyrHxM;

        public ZephyrHxM ZephyrHxM => zephyrHxM;
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
                if (deviceConfiguration.ZephyrHxM.ComPort != null)
                {
                    foreach (string serialPort in SerialPorts)
                    {
                        if (serialPort == deviceConfiguration.ZephyrHxM.ComPort)
                        {
                            zephyrHxM.SerialPort = serialPort;
                            break;
                        }
                    }
                }
                else
                {
                    zephyrHxM.SerialPort = SerialPorts[0];
                }
            }

            udpLogger.Address = logConfiguration.Address;
            udpLogger.Port = logConfiguration.Port;
        }

        public override void SaveConfig(DeviceConfiguration deviceConfiguration)
        {
            deviceConfiguration.Type = Configuration.DeviceConfiguration.DeviceType.ZephyrHxM;
            deviceConfiguration.ZephyrHxM.ComPort = zephyrHxM.SerialPort;
        }
    }
}
