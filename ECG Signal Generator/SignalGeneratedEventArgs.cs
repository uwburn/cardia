using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.ECG_Signal_Generator
{
    public class SignalGeneratedEventArgs : EventArgs
    {
        public int? id;
        public long Time;
        public Signal[] Buffer;

        public SignalGeneratedEventArgs(int? id, long time, Signal[] buffer)
        {
            this.Time = time;
            this.Buffer = buffer;
        }
    }
}
