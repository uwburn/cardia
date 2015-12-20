using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.HRM
{
    public class PacketProcessedEventArgs : EventArgs
    {
        public IHRMPacket HRMPacket;

        public PacketProcessedEventArgs(IHRMPacket hrmPacket)
        {
            this.HRMPacket = hrmPacket;
        }
    }
}
