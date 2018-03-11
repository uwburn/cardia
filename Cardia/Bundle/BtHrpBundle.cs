using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGT.HRM;
using MGT.HRM.HRP;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using System.Threading;
using static MGT.Cardia.Configuration;

namespace MGT.Cardia
{
    public class BtHrpBundle : Bundle
    {
        private readonly BtHrp btHrp = new BtHrp();
        private readonly BtHrpFrm btHrpFrm;
        private readonly BtHrpLoggerCSV csvLogger = new BtHrpLoggerCSV();
        private readonly BtHrpLoggerXLSX xlsxLogger = new BtHrpLoggerXLSX();
        private readonly BtHrpLoggerXML xmlLogger = new BtHrpLoggerXML();

        public DeviceInformationCollection BtSmartDevices { get; private set; }

        public BtHrpBundle()
        {
            var task = DeviceInformation.FindAllAsync(
                GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.HeartRate),
                new string[] { "System.Devices.ContainerId" });

            try
            {
                while (true)
                {
                    Thread.Sleep(100);

                    var status = task.Status;

                    if (status == Windows.Foundation.AsyncStatus.Canceled || task.Status == Windows.Foundation.AsyncStatus.Error)
                        break;

                    if (status == Windows.Foundation.AsyncStatus.Completed)
                    {
                        BtSmartDevices = task.GetResults();
                        break;
                    }
                }
            }
            finally
            {
                task.Close();
            }

            if (BtSmartDevices.Count > 0)
            {
                btHrp.Device = BtSmartDevices[0];
            }

            btHrpFrm = new BtHrpFrm(this);
        }

        public override HeartRateMonitor Device => btHrp;
        public override HRMDeviceFrm DeviceControlForm => btHrpFrm;
        public override IHRMLogger CSVLogger => csvLogger;
        public override IHRMLogger XLSXLogger => xlsxLogger;
        public override IHRMLogger XMLLogger => xmlLogger;
        public override DeviceConfiguration.DeviceType ConfigEnumerator => DeviceConfiguration.DeviceType.BtHrp;

        public BtHrp BtHrp => btHrp;

        public override void InitDevice()
        {
            
        }

        public override void InitControlForm()
        {

        }

        public override void Configure(Configuration.DeviceConfiguration deviceConfiguration)
        {
            if (deviceConfiguration.BtHrp.DeviceId != null)
            {
                foreach (DeviceInformation deviceInformation in BtSmartDevices)
                {
                    if (deviceInformation.Id == deviceConfiguration.BtHrp.DeviceId)
                    {
                        btHrp.Device = deviceInformation;
                        break;
                    }
                }
            }

            btHrp.CharacteristicIndex = deviceConfiguration.BtHrp.CharacteristicIndex;
            btHrp.InitDelay = deviceConfiguration.BtHrp.InitDelay;
        }

        public override void SaveConfig(DeviceConfiguration deviceConfiguration)
        {
            deviceConfiguration.Type = Configuration.DeviceConfiguration.DeviceType.BtHrp;
            if (btHrp.Device != null)
                deviceConfiguration.BtHrp.DeviceId = btHrp.Device.Id;
            deviceConfiguration.BtHrp.CharacteristicIndex = btHrp.CharacteristicIndex;
            deviceConfiguration.BtHrp.InitDelay = btHrp.InitDelay;
        }
    }
}
