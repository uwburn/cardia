using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.Network;

namespace MGT.Cardia
{
    [Serializable]
    public class HeartRateMessage : RelayMessage
    {
        public int? BPM;
        public int? MinBPM;
        public int? MaxBPM;

        public HeartRateMessage() { }

        public HeartRateMessage(int? bpm, int? minBpm, int? maxBpm)
        {
            BPM = bpm;
            MinBPM = minBpm;
            MaxBPM = maxBpm;
        }
    }
}
