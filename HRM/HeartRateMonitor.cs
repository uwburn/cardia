using MGT.Utilities.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGT.HRM
{
    public abstract class HeartRateMonitor : IDisposable
    {
        public abstract string Name { get; }

        // Data
        public abstract IHRMPacket LastPacket { get; protected set; }
        public abstract int TotalPackets { get; protected set; }
        public abstract int CorruptedPackets { get; protected set; }

        public abstract int HeartBeats { get; protected set; }

        public abstract byte? MinHeartRate { get; protected set; }
        public abstract byte? MaxHeartRate { get; protected set; }

        public abstract int HeartRateSmoothingFactor { get; set; }
        public abstract double SmoothedHeartRate { get; protected set; }

        public delegate void PacketProcessedHandler(object sender, PacketProcessedEventArgs e);
        public event PacketProcessedHandler PacketProcessed;

        protected virtual void FirePacketProcessed(PacketProcessedEventArgs e)
        {
            PacketProcessedHandler handler = PacketProcessed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event GenericEventHandler<string> Timeout;

        protected virtual void FireTimeout(string e)
        {
            GenericEventHandler<string> handler = Timeout;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void ResetSubscriptions()
        {
            PacketProcessed = null;
        }

        // Commands
        public abstract bool Running { get; protected set; }
        public abstract void Start();
        public abstract void Stop();
        public abstract void Reset();

        public abstract void Dispose();
    }
}
