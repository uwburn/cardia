using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM.HRP
{
    public abstract class BtHrpLogger : IHRMLogger
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

        protected BtHrp btHrp;

        public void Start(HeartRateMonitor hrm)
        {
            if (hrm is BtHrp)
                btHrp = (BtHrp)hrm;
            else
                throw new Exception("Invalid HRM, BtHrp expected");

            BtHrpStart();

            running = true;

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void BtHrpStart();

        public void Stop()
        {
            if (!running)
                return;
            
            running = false;

            BtHrpStop();

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void BtHrpStop();

        public void Log(IHRMPacket hrmPacket)
        {
            BtHrpPacket btHrpPacket;
            if (hrmPacket is BtHrpPacket)
                btHrpPacket = (BtHrpPacket)hrmPacket;
            else
                throw new Exception("Invalid IHRMPacket, BtHrpPacket expected");

            BtHrpLog(btHrpPacket);
        }

        public void ResetSubscriptions()
        {
            LoggerStatusChanged = null;
            FileNameChanged = null;
        }

        protected abstract void BtHrpLog(BtHrpPacket btHrpPacket);

        public abstract void Dispose();
    }
}
