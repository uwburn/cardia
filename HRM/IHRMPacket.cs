using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.CRC;

namespace MGT.HRM
{
    public interface IHRMPacket
    {
        int HeartRate { get; }
    }
}
