using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.HRM;

namespace MGT.Cardia
{
    public class HRMStatus
    {
        public int? HeartRate { get; private set; }
        public int? MinHeartRate { get; private set; }
        public int? MaxHeartRate { get; private set; }
        public int TotalPackets { get; private set; }

        public HRM.IHRMPacket HRMPacket { get; private set; }

        public HRMStatus(HeartRateMonitor hrm, IHRMPacket hrmPacket)
        {
            HeartRate = hrm.LastPacket.HeartRate;
            MinHeartRate = hrm.MinHeartRate;
            MaxHeartRate = hrm.MaxHeartRate;
            TotalPackets = hrm.TotalPackets;
            HRMPacket = hrmPacket;
        }
    }
}
