using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM.Zephyr_HxM
{
    public abstract class ZephyrHxMLogger : IHRMLogger
    {
        public event GenericEventHandler<bool> LoggerStatusChanged;

        protected bool running = false;
        public bool Running
        {
            get
            {
                return running;
            }
        }

        public event GenericEventHandler<string> FileNameChanged;

        protected string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (running)
                    throw new Exception("Filename cannot be changed when the logger is running");

                string bck = fileName;

                fileName = value;
                if (bck != value)
                    if (FileNameChanged != null)
                        FileNameChanged(this, value);
            }
        }

        protected ZephyrHxM zephyrHxM;

        public void Start(HeartRateMonitor hrm)
        {
            if (hrm is ZephyrHxM)
                zephyrHxM = (ZephyrHxM)hrm;
            else
                throw new Exception("Invalid HRM, ZephyrHxM expected");

            ZephyrStart();

            running = true;

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void ZephyrStart();

        public void Stop()
        {
            if (!running)
                return;
            
            running = false;

            ZephyrStop();

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void ZephyrStop();

        public void Log(IHRMPacket hrmPacket)
        {
            ZephyrPacket zephyrPacket;
            if (hrmPacket is ZephyrPacket)
                zephyrPacket = (ZephyrPacket)hrmPacket;
            else
                throw new Exception("Invalid IHRMPacket, ZephyrPacket expected");

            ZephyrLog(zephyrPacket);
        }

        public void ResetSubscriptions()
        {
            LoggerStatusChanged = null;
            FileNameChanged = null;
        }

        protected abstract void ZephyrLog(ZephyrPacket zephyrPacket);

        public abstract void Dispose();
    }
}
