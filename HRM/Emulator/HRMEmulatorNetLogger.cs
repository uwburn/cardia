using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM.Emulator
{
    public abstract class HRMEmulatorNetLogger : HRMEmulatorLogger, IHRMNetLogger
    {
        public event GenericEventHandler<string> AddressChanged;
        public event GenericEventHandler<int> SourcePortChanged;
        public event GenericEventHandler<int> PortChanged;

        protected string ipAddress;
        public string Address
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (running)
                    throw new Exception("IP address cannot be changed when the logger is running");

                string bck = ipAddress;

                ipAddress = value;
                if (bck != value)
                    if (AddressChanged != null)
                        AddressChanged(this, value);
            }
        }

        protected int sourcePort;
        public int SourcePort
        {
            get
            {
                return sourcePort;
            }
            set
            {
                if (running)
                    throw new Exception("Source port cannot be changed when the logger is running");

                int bck = sourcePort;

                sourcePort = value;
                if (bck != value)
                    if (AddressChanged != null)
                        SourcePortChanged(this, value);
            }
        }

        protected int destinationPort;
        public int Port
        {
            get
            {
                return destinationPort;
            }
            set
            {
                if (running)
                    throw new Exception("IP address cannot be changed when the logger is running");

                int bck = destinationPort;

                destinationPort = value;
                if (bck != value)
                    if (AddressChanged != null)
                        PortChanged(this, value);
            }
        }

        public override void ResetSubscriptions()
        {
            base.ResetSubscriptions();
            AddressChanged = null;
            SourcePortChanged = null;
            PortChanged = null;
        }
    }
}
