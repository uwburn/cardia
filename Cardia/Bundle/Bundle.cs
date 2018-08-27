using MGT.HRM;
using MGT.Utilities.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGT.Cardia.Configuration;

namespace MGT.Cardia
{
    public abstract class Bundle : IDisposable
    {
        public abstract HeartRateMonitor Device { get; }
        public abstract HRMDeviceFrm DeviceControlForm { get; }
        public abstract IHRMFileLogger CSVLogger { get; }
        public abstract IHRMFileLogger XLSXLogger { get; }
        public abstract IHRMFileLogger XMLLogger { get; }
        public abstract IHRMNetLogger UDPLogger { get; }
        public abstract DeviceConfiguration.DeviceType ConfigEnumerator { get; }

        public event GenericEventHandler Started;
        public event GenericEventHandler Stopped;

        public abstract void InitDevice();
        public abstract void InitControlForm();
        public abstract void LoadConfig(Configuration.DeviceConfiguration deviceConfiguration, Configuration.LogConfiguration logConfiguration);
        public abstract void SaveConfig(Configuration.DeviceConfiguration deviceConfiguration);

        public void RegisterCardiaEventHandlers(Cardia cardia)
        {
            cardia.Started += Cardia_Started;
            cardia.Stopped += Cardia_Stopped;
        }

        private void Cardia_Started(object sender)
        {
            Started(sender);
        }

        private void Cardia_Stopped(object sender)
        {
            Stopped(sender);
        }

        public void Dispose()
        {
            Device.Dispose();
        }
    }
}
