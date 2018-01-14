using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.HRM.HRP
{
    public class BtHrpPacket : IHRMPacket
    {
        public int HeartRate { get; set; }
        public bool HasExpendedEnergy { get; set; }
        public int ExpendedEnergy { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public override string ToString()
        {
            return base.ToString() + "[ HeartRate = " + HeartRate + " ]";
        }

    }
}
