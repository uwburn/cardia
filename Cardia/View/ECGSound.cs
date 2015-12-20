using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;
using MGT.ECG_Signal_Generator;
using System.Threading;
using System.Media;
using System.IO;
using System.Reflection;

namespace MGT.Cardia
{
    class ECGSound : IDisposable
    {
        private Cardia cardia;

        private bool active = true;

        private bool playBeat = true;
        private bool playAlarm = true;
        private int volume = 10;

        private bool edge = true;
        private bool alarm = false;
        private bool alarmPlaying = false;

        SoundPlayer beep1 = new SoundPlayer(Resources.Beep1);
        SoundPlayer beep2 = new SoundPlayer(Resources.Beep2);
        SoundPlayer beep3 = new SoundPlayer(Resources.Beep3);
        SoundPlayer beep4 = new SoundPlayer(Resources.Beep4);
        SoundPlayer beep5 = new SoundPlayer(Resources.Beep5);
        SoundPlayer beep6 = new SoundPlayer(Resources.Beep6);
        SoundPlayer beep7 = new SoundPlayer(Resources.Beep7);
        SoundPlayer beep8 = new SoundPlayer(Resources.Beep8);
        SoundPlayer beep9 = new SoundPlayer(Resources.Beep9);
        SoundPlayer beep10 = new SoundPlayer(Resources.Beep10);

        SoundPlayer alarm1 = new SoundPlayer(Resources.Alarm1);
        SoundPlayer alarm2 = new SoundPlayer(Resources.Alarm2);
        SoundPlayer alarm3 = new SoundPlayer(Resources.Alarm3);
        SoundPlayer alarm4 = new SoundPlayer(Resources.Alarm4);
        SoundPlayer alarm5 = new SoundPlayer(Resources.Alarm5);
        SoundPlayer alarm6 = new SoundPlayer(Resources.Alarm6);
        SoundPlayer alarm7 = new SoundPlayer(Resources.Alarm7);
        SoundPlayer alarm8 = new SoundPlayer(Resources.Alarm8);
        SoundPlayer alarm9 = new SoundPlayer(Resources.Alarm9);
        SoundPlayer alarm10 = new SoundPlayer(Resources.Alarm10);

        Thread worker;

        public ECGSound(Cardia cardia)
        {
            this.cardia = cardia;

            worker = new Thread(DoWork);
            worker.Name = "ECG Sound Worker";
            worker.Start();
            
            cardia.SignalGenerated += cardia_SignalGenerated;
            cardia.AlarmTripped += cardia_AlarmTripped;
            cardia.PlayBeatChanged += cardia_PlayBeatChanged;
            cardia.PlayAlarmChanged += cardia_PlayAlarmChanged;
            cardia.VolumeChanged += cardia_VolumeChanged;
        }

        void cardia_PlayBeatChanged(object sender, bool arg)
        {
            playBeat = arg;
        }

        void cardia_PlayAlarmChanged(object sender, bool arg)
        {
            playAlarm = arg;
            if (alarmPlaying)
            {
                StopAlarm();
                PlayAlarm();
            }
        }

        void cardia_SignalGenerated(object sender, SignalGeneratedEventArgs e)
        {
            if (e.Buffer[0].Beat && edge)
            {
                if (edge)
                    edge = false;

                lock (this)
                    Monitor.Pulse(this);
            }
            else
            {
                edge = true;
            }
        }

        void cardia_AlarmTripped(object sender, bool arg)
        {
            alarm = arg;

            if (arg)
                lock (this)
                    Monitor.Pulse(this);
            else
                StopAlarm();
        }

        void cardia_VolumeChanged(object sender, int arg)
        {
            volume = arg;

            if (alarmPlaying)
            {
                StopAlarm();
                PlayAlarm();
            }

        }

        private void DoWork()
        {
            while (active)
            {
                lock (this)
                    Monitor.Wait(this);

                if (active)
                    PlaySound();
            }
        }

        private void PlaySound()
        {
            if (alarm && playAlarm)
            {
                PlayAlarm();
            }
            else
            {
                StopAlarm();
                PlayBeep();
            }
        }

        private void PlayAlarm()
        {
            if (!playAlarm)
                return;

            if (alarmPlaying)
                return;

            switch (volume)
            {
                case 1:
                    alarm1.PlayLooping();
                    break;
                case 2:
                    alarm2.PlayLooping();
                    break;
                case 3:
                    alarm3.PlayLooping();
                    break;
                case 4:
                    alarm4.PlayLooping();
                    break;
                case 5:
                    alarm5.PlayLooping();
                    break;
                case 6:
                    alarm6.PlayLooping();
                    break;
                case 7:
                    alarm7.PlayLooping();
                    break;
                case 8:
                    alarm8.PlayLooping();
                    break;
                case 9:
                    alarm9.PlayLooping();
                    break;
                case 10:
                    alarm10.PlayLooping();
                    break;
            }

            alarmPlaying = true;
        }

        private void StopAlarm()
        {
            alarm1.Stop();
            alarm2.Stop();
            alarm3.Stop();
            alarm4.Stop();
            alarm5.Stop();
            alarm6.Stop();
            alarm7.Stop();
            alarm8.Stop();
            alarm9.Stop();
            alarm10.Stop();

            alarmPlaying = false;
        }

        private void PlayBeep()
        {
            if (!playBeat)
                return;

            switch (volume)
            {
                case 1:
                    beep1.PlaySync();
                    break;
                case 2:
                    beep2.PlaySync();
                    break;
                case 3:
                    beep3.PlaySync();
                    break;
                case 4:
                    beep4.PlaySync();
                    break;
                case 5:
                    beep5.PlaySync();
                    break;
                case 6:
                    beep6.PlaySync();
                    break;
                case 7:
                    beep7.PlaySync();
                    break;
                case 8:
                    beep8.PlaySync();
                    break;
                case 9:
                    beep9.PlaySync();
                    break;
                case 10:
                    beep10.PlaySync();
                    break;
            }
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                active = false;

                lock (this)
                    Monitor.Pulse(this);

                worker.Abort();

                alarm1.Dispose();
                alarm2.Dispose();
                alarm3.Dispose();
                alarm4.Dispose();
                alarm5.Dispose();
                alarm6.Dispose();
                alarm7.Dispose();
                alarm8.Dispose();
                alarm9.Dispose();
                alarm10.Dispose();

                beep1.Dispose();
                beep2.Dispose();
                beep3.Dispose();
                beep4.Dispose();
                beep5.Dispose();
                beep6.Dispose();
                beep7.Dispose();
                beep8.Dispose();
                beep9.Dispose();
                beep10.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
