using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.ECG_Signal_Generator;
using MGT.Utilities.Collections;
using System.Drawing.Drawing2D;

namespace MGT.Cardia
{
    public partial class ECGDisplay : UserControl
    {
        int interval = 20;
        int chartTime = 4000;
        float step;
        int chartPoints;

        int offset = 40;
        int brushSize = 8;
        int midHeight;
        Image image;

        Image heart = new Bitmap(24, 24);

        bool showNickname = true;
        string nickname;

        bool beat;

        int? bpm;
        int? minBpm;
        int? maxBpm;
        bool alarm;

        Color backColor = Color.Black;
        Color selectedColor = Color.Lime;
        Brush brush = Brushes.Lime;
        Pen pen;

        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }

        public int ChartTime
        {
            get
            {
                return chartTime;
            }
            set
            {
                chartTime = value;
                step = (image.Width) * interval / (float)chartTime;
                chartPoints = chartTime / interval;
            }
        }

        public int Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
            }
        }

        public int BrushSize
        {
            get
            {
                return brushSize;
            }
            set
            {
                brushSize = value;
            }
        }

        public Color Color
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;

                ApplyColor();
            }
        }

        public bool ShowNickname
        {
            get
            {
                return showNickname;
            }
            set
            {
                showNickname = value;
                lbNickname.Visible = showNickname;
            }
        }

        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
                lbNickname.Text = nickname ?? "-";
            }
        }

        public bool Beat
        {
            get
            {
                return beat;
            }
            set
            {
                beat = value;
                pbHeartBeat.Visible = beat;
            }
        }

        public int? BPM
        {
            get
            {
                return bpm;
            }
            set
            {
                bpm = value;
                if (bpm != null)
                    lbBPM.Text = bpm.ToString();
                else
                    lbBPM.Text = "-";
            }
        }

        public int? MinBPM
        {
            get
            {
                return minBpm;
            }
            set
            {
                minBpm = value;
                if (minBpm != null)
                    lbMinBPM.Text = minBpm.ToString();
                else
                    lbMinBPM.Text = "-";
            }
        }

        public int? MaxBPM
        {
            get
            {
                return maxBpm;
            }
            set
            {
                maxBpm = value;
                if (maxBpm != null)
                    lbMaxBPM.Text = maxBpm.ToString();
                else
                    lbMaxBPM.Text = "-";
            }
        }

        public bool Alarm
        {
            get
            {
                return alarm;
            }
            set
            {
                alarm = value;

                lbAlarm.Visible = alarm;
                lbBPMDesc.Visible = !alarm;
            }
        }

        public ECGDisplay()
        {
            InitializeComponent();

            InitializeECGChartImage();
            ApplyColor();

            ChartTime = chartTime;
        }

        private void ApplyColor()
        {
            this.BackColor = selectedColor;
            pnlSeparator.BackColor = selectedColor;
            lbNickname.ForeColor = selectedColor;
            lbBPM.ForeColor = selectedColor;
            lbBPMDesc.ForeColor = selectedColor;
            lbAlarm.ForeColor = selectedColor;
            lbMinBPM.ForeColor = selectedColor;
            lbMinBPMDesc.ForeColor = selectedColor;
            lbMaxBPM.ForeColor = selectedColor;
            lbMaxBPMDesc.ForeColor = selectedColor;

            brush = new SolidBrush(selectedColor);
            pen = new Pen(brush, 2);

            DrawHeart();
        }

        private void InitializeECGChartImage()
        {
            image = new Bitmap(pbECGPlot.DisplayRectangle.Width, pbECGPlot.DisplayRectangle.Height);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(backColor);
            }
            midHeight = image.Height / 2;
            pbECGPlot.BackColor = backColor;
        }

        private void DrawHeart()
        {
            heart = new Bitmap(24, 24);

            using (Graphics g = Graphics.FromImage(heart))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.Clear(backColor);

                g.FillEllipse(brush, 2, 2, 10, 10);
                g.FillEllipse(brush, 12, 2, 10, 10);
                g.FillPolygon(brush, new Point[] { new Point(2, 8), new Point(22, 8), new Point(12, 22) });
            }

            pbHeartBeat.Image = heart;
        }

        public void Push(long time, Signal[] buffer)
        {
            if (buffer == null)
                return;

            UpdateECG(time, buffer);
        }

        public void Push(long time, Signal[] buffer, int bpm, int minBpm, int maxBpm)
        {
            if (buffer == null)
                return;

            this.SuspendLayout();

            BPM = bpm;
            MinBPM = minBpm;
            MaxBPM = maxBpm;

            UpdateECG(time, buffer);

            this.ResumeLayout();
        }

        private void UpdateECG(long time, Signal[] buffer)
        {
            this.SuspendLayout();

            if (buffer[0].Beat)
                pbHeartBeat.Visible = true;
            else
                pbHeartBeat.Visible = false;

            using (Graphics g = Graphics.FromImage(image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(backColor);

                int startingBin = (int)(time % chartPoints);

                List<PointF> points1 = new List<PointF>(chartPoints + 1 - brushSize);
                List<byte> pointsType1 = new List<byte>(chartPoints + 1 - brushSize);
                List<PointF> points2 = new List<PointF>(chartPoints + 1 - brushSize);
                List<byte> pointsType2 = new List<byte>(chartPoints + 1 - brushSize);
                for (int i = 0; i < chartPoints + 1 - brushSize; i++)
                {
                    float x = (startingBin - i) * step;
                    float y = buffer[i].Value * midHeight * 2 + midHeight + offset;
                    if (x > 0)
                    {
                        PointF point = new PointF(x, y);
                        points2.Add(point);
                        pointsType2.Add(1);
                    }
                    else if (x < 0)
                    {
                        PointF point = new PointF(pbECGPlot.DisplayRectangle.Width + x, y);
                        points1.Add(point);
                        pointsType1.Add(1);
                    }
                    else
                    {
                        PointF point0 = new PointF(0, y);
                        points2.Add(point0);
                        pointsType2.Add(1);

                        PointF pointW = new PointF(pbECGPlot.DisplayRectangle.Width, y);
                        points1.Add(pointW);
                        pointsType1.Add(1);
                    }
                }

                if (points1.Count > 0)
                {
                    using (GraphicsPath path = new GraphicsPath(points1.ToArray(), pointsType1.ToArray()))
                    {
                        g.DrawPath(pen, path);
                    }
                }

                if (points2.Count > 0)
                {
                    using (GraphicsPath path = new GraphicsPath(points2.ToArray(), pointsType2.ToArray()))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            pbECGPlot.Image = image;

            this.ResumeLayout();
        }

        private void pbECGPlot_Resize(object sender, EventArgs e)
        {
            InitializeECGChartImage();

            step = (image.Width) * interval / (float)chartTime;
        }

        public void Reset(bool clearNickname)
        {
            if (clearNickname)
            {
                ShowNickname = false;
                Nickname = "";
            }
            BPM = null;
            MinBPM = null;
            MaxBPM = null;
            Beat = false;

            using (Graphics g = Graphics.FromImage(image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(backColor);
            }

            pbECGPlot.Image = image;
        }

        private void route_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void route_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        private void route_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        private void route_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void route_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }
    }
}
