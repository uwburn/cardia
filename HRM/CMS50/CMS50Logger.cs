using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM.CMS50
{
    public abstract class CMS50Logger : IHRMLogger
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

        protected CMS50 cms50;

        public void Start(HeartRateMonitor hrm)
        {
            if (hrm is CMS50)
                cms50 = (CMS50)hrm;
            else
                throw new Exception("Invalid HRM, CMS50 expected");

            CMS50Start();

            running = true;

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void CMS50Start();

        public void Stop()
        {
            if (!running)
                return;
            
            running = false;

            CMS50Stop();

            if (LoggerStatusChanged != null)
                LoggerStatusChanged(this, running);
        }

        protected abstract void CMS50Stop();

        public void Log(IHRMPacket hrmPacket)
        {
            CMS50Packet cms50Packet;
            if (hrmPacket is CMS50Packet)
                cms50Packet = (CMS50Packet)hrmPacket;
            else
                throw new Exception("Invalid IHRMPacket, CMS50Packet expected");

            CMS50Log(cms50Packet);
        }

        public virtual void ResetSubscriptions()
        {
            LoggerStatusChanged = null;
        }

        protected abstract void CMS50Log(CMS50Packet cms50Packet);

        public abstract void Dispose();
    }
}
