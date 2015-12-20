using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MGT.HRM;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    class HRMAlarm : IDisposable
    {
        // Fields
        private bool enabled;
        private bool alarm = false;
        private int lowThreshold;
        private int highThreshold;

        private bool defuse;
        private bool defused;
        private TimeSpan defuseTime;
        private DateTime? alarmStartTime;
        private Timer defuseTimer = new Timer(1000);

        private int bpm;

        // Properties
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled && alarm && !value)
                    if (AlarmChanged != null)
                        AlarmChanged(this, false);

                enabled = value;
            }
        }

        public int LowThreshold
        {
            get { return lowThreshold; }
            set
            {
                lowThreshold = value;
                ResetAlarm();
            }
        }

        public int HighThreshold
        {
            get { return highThreshold; }
            set
            {
                highThreshold = value;
                ResetAlarm();
            }
        }

        public bool Defuse
        {
            get { return defuse; }
            set
            {
                defuse = value;
                ResetAlarm();
            }
        }

        public int DefuseTime
        {
            get { return defuseTime.Minutes * 60 + defuseTime.Seconds; }
            set
            {
                if (value < 0)
                    value = 0;

                if (value > 3600)
                    value = 3600;

                defuseTime = new TimeSpan(0, 0, value);
            }
        }

        public int BPM
        {
            get { return bpm; }
            set
            {
                bpm = value;
                CheckBPM();
            }
        }

        // Events
        public event GenericEventHandler<bool> AlarmChanged;

        // Constructor
        public HRMAlarm()
        {
            defuseTimer.Elapsed += new ElapsedEventHandler(defuseTimer_Elapsed);
            defuseTimer.Start();
        }

        // Methods
        void CheckBPM()
        {
            if (bpm >= highThreshold)
                FireAlarm();
            else if (bpm <= lowThreshold)
                FireAlarm();
            else
                CeaseAlarm();
        }

        private void FireAlarm()
        {
            if (alarmStartTime == null)
            {
                defused = false;
                alarmStartTime = DateTime.Now;

                alarm = true;
                if (AlarmChanged != null)
                    AlarmChanged(this, true);
            }
        }

        private void CeaseAlarm()
        {
            defused = false;
            alarmStartTime = null;

            alarm = false;
            if (AlarmChanged != null)
                AlarmChanged(this, false);
        }

        private void ResetAlarm()
        {
            alarmStartTime = null;
            defused = false;
            alarm = false;
        }

        void defuseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!defuse)
                return;

            if (defused)
                return;

            if (alarmStartTime == null)
                return;

            if (DateTime.Now - (alarmStartTime ?? DateTime.Now) < defuseTime)
                return;

            defused = true;

            alarm = false;
            if (AlarmChanged != null)
                AlarmChanged(this, false);
        }

        // IDisposable
        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if(_disposed)
                return;

            if (disposing)
            {
                defuseTimer.Stop();
                defuseTimer.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
