using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.Collections;
using MGT.Utilities.Timer;
using System.Drawing;
using System.Threading;

namespace MGT.ECG_Signal_Generator
{
    public class SignalGenerator
    {
        private int? id;
        private Thread workerThread;
        private int internalInterval;
        private long lastElapsed;
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private Random random = new Random();

        private Signal[] ecg;
        private int threshold;

        private Signal[] ecg20_1;
        private Signal[] ecg20_2;
        private Signal[] ecg20_3;
        private Signal[] ecg20_4;
        private Signal[] ecg18_1;
        private Signal[] ecg18_2;
        private Signal[] ecg18_3;
        private Signal[] ecg18_4;
        private Signal[] ecg16_1;
        private Signal[] ecg16_2;
        private Signal[] ecg16_3;
        private Signal[] ecg16_4;
        private Signal[] ecg13_1;
        private Signal[] ecg13_2;
        private Signal[] ecg13_3;
        private Signal[] ecg13_4;
        private Signal[] ecg12_1;
        private Signal[] ecg12_2;
        private Signal[] ecg12_3;
        private Signal[] ecg12_4;

        private volatile bool running;
        public int Interval { get; private set; }
        public long Time { get; private set; }
        private long offset = 0;
        private int bufferTime;
        private CircularBuffer<Signal> buffer;
        int rrInterval = 0;
        int nextRrInterval = 0;

        public int BPM 
        { 
            get 
            {
                if (rrInterval == 0)
                    return 0;
                else
                    return 60000 / rrInterval / Interval;
            } 
            set 
            {
                if (value > 250)
                    value = 250;

                if (value == 0)
                    nextRrInterval = 0;
                else
                    nextRrInterval = 60000 / value / Interval;
            } 
        }

        public Signal[] Buffer
        {
            get
            {
                return buffer.ToArray();
            }
        }

        public delegate void SignalGeneratedHandler(object sender, SignalGeneratedEventArgs e);
        public event SignalGeneratedHandler OnSignalGenerated;

        public SignalGenerator(int? id, int bufferTime)
        {
            this.id = id;
            Interval = 20;
            internalInterval = Interval;
            this.bufferTime = bufferTime / Interval;

            LoadECGs();
            NextECG();
        }

        public SignalGenerator(int bufferTime) : this(null, bufferTime) { }

        private void LoadECGs()
        {
            LoadECG(ECGBitmaps.ECG_20_1, out ecg20_1);
            LoadECG(ECGBitmaps.ECG_20_2, out ecg20_2);
            LoadECG(ECGBitmaps.ECG_20_3, out ecg20_3);
            LoadECG(ECGBitmaps.ECG_20_4, out ecg20_4);
            LoadECG(ECGBitmaps.ECG_18_1, out ecg18_1);
            LoadECG(ECGBitmaps.ECG_18_2, out ecg18_2);
            LoadECG(ECGBitmaps.ECG_18_3, out ecg18_3);
            LoadECG(ECGBitmaps.ECG_18_4, out ecg18_4);
            LoadECG(ECGBitmaps.ECG_16_1, out ecg16_1);
            LoadECG(ECGBitmaps.ECG_16_2, out ecg16_2);
            LoadECG(ECGBitmaps.ECG_16_3, out ecg16_3);
            LoadECG(ECGBitmaps.ECG_16_4, out ecg16_4);
            LoadECG(ECGBitmaps.ECG_13_1, out ecg13_1);
            LoadECG(ECGBitmaps.ECG_13_2, out ecg13_2);
            LoadECG(ECGBitmaps.ECG_13_3, out ecg13_3);
            LoadECG(ECGBitmaps.ECG_13_4, out ecg13_4);
            LoadECG(ECGBitmaps.ECG_12_1, out ecg12_1);
            LoadECG(ECGBitmaps.ECG_12_2, out ecg12_2);
            LoadECG(ECGBitmaps.ECG_12_3, out ecg12_3);
            LoadECG(ECGBitmaps.ECG_12_4, out ecg12_4);
        }

        private void NextECG()
        {
            int i = random.Next(0, 4);
            if (rrInterval < 14)
            {
                switch (i)
                {
                    case 0:
                        ecg = ecg12_1;
                        break;
                    case 1:
                        ecg = ecg12_2;
                        break;
                    case 2:
                        ecg = ecg12_3;
                        break;
                    case 3:
                        ecg = ecg12_4;
                        break;
                }
                threshold = 12;
            }
            else if (rrInterval < 16)
            {
                switch (i)
                {
                    case 0:
                        ecg = ecg13_1;
                        break;
                    case 1:
                        ecg = ecg13_2;
                        break;
                    case 2:
                        ecg = ecg13_3;
                        break;
                    case 3:
                        ecg = ecg13_4;
                        break;
                }
                threshold = 13;
            }
            else if (rrInterval < 18)
            {
                switch (i)
                {
                    case 0:
                        ecg = ecg16_1;
                        break;
                    case 1:
                        ecg = ecg16_2;
                        break;
                    case 2:
                        ecg = ecg16_3;
                        break;
                    case 3:
                        ecg = ecg16_4;
                        break;
                }
                threshold = 16;
            }
            else if (rrInterval < 20)
            {
                switch (i)
                {
                    case 0:
                        ecg = ecg18_1;
                        break;
                    case 1:
                        ecg = ecg18_2;
                        break;
                    case 2:
                        ecg = ecg18_3;
                        break;
                    case 3:
                        ecg = ecg18_4;
                        break;
                }
                threshold = 18;
            }
            else
            {
                switch (i)
                {
                    case 0:
                        ecg = ecg20_1;
                        break;
                    case 1:
                        ecg = ecg20_2;
                        break;
                    case 2:
                        ecg = ecg20_3;
                        break;
                    case 3:
                        ecg = ecg20_4;
                        break;
                }
                threshold = 20;
            }
        }

        private void LoadECG(Bitmap bmp, out Signal[] ecg)
        {
            ecg = new Signal[bmp.Width];
            int midHeight = bmp.Height / 2;
            for (int i = 0; i < bmp.Width; i++)
            {
                int? height = null;
                bool beat = false;
                bool peak = false;

                for (int j = bmp.Height - 1; j >= 0; j--)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (color.G == 255)
                    {
                        beat = true;
                    }
                    if (color.B == 255)
                    {
                        peak = true;
                    }
                    if (color.R == 255)
                    {
                        height = j;
                        break;
                    }
                }

                float value = ((height ?? midHeight) - midHeight) / (float)bmp.Height;
                ecg[i] = new Signal(value, beat, peak);
            }
        }

        private Signal ECG(long time)
        {
            Signal value;
            if (rrInterval == 0)
            {
                value = Signal.Zero;

                if (nextRrInterval != rrInterval)
                    rrInterval = nextRrInterval;
            }
            else
            {
                NextECG();

                int i = (int)(time + offset) % rrInterval; // intervallo tra le pulsazioni diviso realInterval
                if (i < threshold)
                {
                    value = ecg[i];
                }
                else
                {
                    value = Signal.Zero;
                }

                if (nextRrInterval != rrInterval)
                {
                    if (i == rrInterval - 1)
                    {
                        if (nextRrInterval != 0)
                        {
                            while (((int)(time + offset) % nextRrInterval) != 0)
                            {
                                if (offset > nextRrInterval)
                                    offset = 0;
                                offset++;
                            }
                        }

                        rrInterval = nextRrInterval;
                    }
                }
            }

            return value;
        }

        public void Start()
        {
            if (!running)
            {
                buffer = new CircularBuffer<Signal>(bufferTime);
                running = true;
                internalInterval = Interval;
                lastElapsed = 0;
                Time = 0;
                workerThread = new Thread(DoWork);
                workerThread.Name = "ECG Signal Generator Worker";
                workerThread.Start();
                stopwatch.Start();
            }
        }

        public void Stop()
        {
            if (running)
            {
                running = false;
                //workerThread.Abort();
                stopwatch.Reset();
                Thread.Sleep(internalInterval * 2);
                nextRrInterval = 0;
                rrInterval = 0;
            }
        }

        public void ResetSubscriptions()
        {
            OnSignalGenerated = null;
        }

        public void DoWork()
        {
            while (running)
            {
                Signal value = ECG(Time);
                buffer.Add(value);

                Time++;

                if (OnSignalGenerated != null)
                {
                    SignalGeneratedEventArgs args = new SignalGeneratedEventArgs(id, Time, Buffer);
                    OnSignalGenerated(this, args);
                }

                Thread.Sleep(internalInterval);

                long elapsed = stopwatch.ElapsedMilliseconds;
                int currentInterval = (int)(elapsed - lastElapsed);
                int delta = Interval - currentInterval;
                lastElapsed = elapsed;

                if (internalInterval + delta > 0)
                    internalInterval += delta;
            }
        }
    }
}
