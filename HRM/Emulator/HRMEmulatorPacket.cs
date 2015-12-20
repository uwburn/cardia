using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.HRM.Emulator
{
    public class HRMEmulatorPacket : IHRMPacket
    {
        private int heartRate;

        public HRMEmulatorPacket(int heartRate)
        {
            this.heartRate = heartRate;
        }

        public int HeartRate
        {
            get { return heartRate; }
        }
    }
}
