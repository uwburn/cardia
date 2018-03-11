using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MGT.HRM;
using MGT.HRM.Emulator;
using static MGT.Cardia.Configuration;

namespace MGT.Cardia
{
    public class HRMEmulatorBundle : Bundle
    {
        private readonly HRMEmulator hrmEmulator = new HRMEmulator();
        private readonly HRMEmulatorFrm hrmEmulatorFrm;
        private readonly HRMEmulatorLoggerCSV csvLogger = new HRMEmulatorLoggerCSV();
        private readonly HRMEmulatorLoggerXLSX xlsxLogger = new HRMEmulatorLoggerXLSX();
        private readonly HRMEmulatorLoggerXML xmlLogger = new HRMEmulatorLoggerXML();

        public HRMEmulatorBundle()
        {
            this.hrmEmulatorFrm = new HRMEmulatorFrm(this);
        }

        public override HeartRateMonitor Device => hrmEmulator;
        public override HRMDeviceFrm DeviceControlForm => hrmEmulatorFrm;
        public override IHRMLogger CSVLogger => csvLogger;
        public override IHRMLogger XLSXLogger => xlsxLogger;
        public override IHRMLogger XMLLogger => xmlLogger;
        public override DeviceConfiguration.DeviceType ConfigEnumerator => DeviceConfiguration.DeviceType.HRMEmulator;

        public HRMEmulator HRMEmulator => hrmEmulator;

        public override void InitDevice()
        {

        }

        public override void InitControlForm()
        {

        }

        public override void LoadConfig(Configuration.DeviceConfiguration deviceConfiguration)
        {
            hrmEmulator.EmulatorMinBPM = deviceConfiguration.HRMEmulator.Min;
            hrmEmulator.EmulatorMaxBPM = deviceConfiguration.HRMEmulator.Max;
        }

        public override void SaveConfig(DeviceConfiguration deviceConfiguration)
        {
            deviceConfiguration.Type = Configuration.DeviceConfiguration.DeviceType.HRMEmulator;
            deviceConfiguration.HRMEmulator.Min = hrmEmulator.EmulatorMinBPM;
            deviceConfiguration.HRMEmulator.Max = hrmEmulator.EmulatorMaxBPM;
        }
    }
}
