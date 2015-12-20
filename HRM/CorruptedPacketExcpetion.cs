using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.HRM
{
    class CorruptedPacketExcpetion : Exception
    {
        public CorruptedPacketExcpetion(string message) : base(message) { }
    }
}
