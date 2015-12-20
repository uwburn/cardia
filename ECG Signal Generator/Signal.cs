using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.ECG_Signal_Generator
{
    public struct Signal
    {
        public float Value;
        public bool Beat;
        public bool Peak;

        public Signal(float value, bool beat, bool peak)
        {
            this.Value = value;
            this.Beat = beat;
            this.Peak = peak;
        }

        public static readonly Signal Zero = new Signal(0f, false, false);
    }
}
