using MGT.HRM;
using MGT.Utilities.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MGT.Cardia
{
    class NetworkSampler
    {
        private Cardia cardia;

        private Timer networkTimer = new Timer();

        public event GenericEventHandler<IHRMPacket, byte?, byte?> PacketSampled;

        public NetworkSampler(Cardia cardia)
        {
            this.cardia = cardia;

            networkTimer.Interval = 1000;
            networkTimer.Elapsed += networkTimer_Elapsed;
        }

        public void Start()
        {
            networkTimer.Start();
        }

        public void Stop()
        {
            networkTimer.Stop();
        }

        void networkTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!cardia.Bundle.Device.Running)
                return;

            if (cardia.Bundle.Device.LastPacket == null)
                return;

            if (PacketSampled != null)
                PacketSampled(this, cardia.Bundle.Device.LastPacket, cardia.Bundle.Device.MinHeartRate, cardia.Bundle.Device.MaxHeartRate);
        }
    }
}
