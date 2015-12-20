namespace MGT.Cardia
{
    partial class ECGDisplay
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMaxBPM = new System.Windows.Forms.Label();
            this.lbMinBPM = new System.Windows.Forms.Label();
            this.lbMaxBPMDesc = new System.Windows.Forms.Label();
            this.pbECGPlot = new System.Windows.Forms.PictureBox();
            this.pbHeartBeat = new System.Windows.Forms.PictureBox();
            this.lbMinBPMDesc = new System.Windows.Forms.Label();
            this.lbBPM = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lbAlarm = new System.Windows.Forms.Label();
            this.lbBPMDesc = new System.Windows.Forms.Label();
            this.lbNickname = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlSeparator = new System.Windows.Forms.Panel();
            this.pnlTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbECGPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeartBeat)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlSeparator.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbMaxBPM
            // 
            this.lbMaxBPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMaxBPM.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaxBPM.ForeColor = System.Drawing.Color.Lime;
            this.lbMaxBPM.Location = new System.Drawing.Point(375, 7);
            this.lbMaxBPM.Margin = new System.Windows.Forms.Padding(3);
            this.lbMaxBPM.Name = "lbMaxBPM";
            this.lbMaxBPM.Size = new System.Drawing.Size(48, 24);
            this.lbMaxBPM.TabIndex = 13;
            this.lbMaxBPM.Text = "-";
            this.lbMaxBPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbMaxBPM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbMaxBPM.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbMaxBPM.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbMaxBPM.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbMaxBPM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbMinBPM
            // 
            this.lbMinBPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMinBPM.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinBPM.ForeColor = System.Drawing.Color.Lime;
            this.lbMinBPM.Location = new System.Drawing.Point(240, 7);
            this.lbMinBPM.Margin = new System.Windows.Forms.Padding(3);
            this.lbMinBPM.Name = "lbMinBPM";
            this.lbMinBPM.Size = new System.Drawing.Size(48, 24);
            this.lbMinBPM.TabIndex = 12;
            this.lbMinBPM.Text = "-";
            this.lbMinBPM.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lbMinBPM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbMinBPM.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbMinBPM.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbMinBPM.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbMinBPM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbMaxBPMDesc
            // 
            this.lbMaxBPMDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMaxBPMDesc.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMaxBPMDesc.ForeColor = System.Drawing.Color.Lime;
            this.lbMaxBPMDesc.Location = new System.Drawing.Point(313, 7);
            this.lbMaxBPMDesc.Margin = new System.Windows.Forms.Padding(3);
            this.lbMaxBPMDesc.Name = "lbMaxBPMDesc";
            this.lbMaxBPMDesc.Size = new System.Drawing.Size(56, 24);
            this.lbMaxBPMDesc.TabIndex = 11;
            this.lbMaxBPMDesc.Text = "Max:";
            this.lbMaxBPMDesc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbMaxBPMDesc.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbMaxBPMDesc.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbMaxBPMDesc.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbMaxBPMDesc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // pbECGPlot
            // 
            this.pbECGPlot.BackColor = System.Drawing.Color.Black;
            this.pbECGPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbECGPlot.Location = new System.Drawing.Point(0, 2);
            this.pbECGPlot.Margin = new System.Windows.Forms.Padding(0);
            this.pbECGPlot.Name = "pbECGPlot";
            this.pbECGPlot.Size = new System.Drawing.Size(421, 104);
            this.pbECGPlot.TabIndex = 3;
            this.pbECGPlot.TabStop = false;
            this.pbECGPlot.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.pbECGPlot.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.pbECGPlot.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.pbECGPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.pbECGPlot.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            this.pbECGPlot.Resize += new System.EventHandler(this.pbECGPlot_Resize);
            // 
            // pbHeartBeat
            // 
            this.pbHeartBeat.Location = new System.Drawing.Point(5, 6);
            this.pbHeartBeat.Name = "pbHeartBeat";
            this.pbHeartBeat.Size = new System.Drawing.Size(24, 24);
            this.pbHeartBeat.TabIndex = 33;
            this.pbHeartBeat.TabStop = false;
            this.pbHeartBeat.Visible = false;
            this.pbHeartBeat.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.pbHeartBeat.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.pbHeartBeat.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.pbHeartBeat.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.pbHeartBeat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbMinBPMDesc
            // 
            this.lbMinBPMDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMinBPMDesc.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinBPMDesc.ForeColor = System.Drawing.Color.Lime;
            this.lbMinBPMDesc.Location = new System.Drawing.Point(178, 7);
            this.lbMinBPMDesc.Margin = new System.Windows.Forms.Padding(3);
            this.lbMinBPMDesc.Name = "lbMinBPMDesc";
            this.lbMinBPMDesc.Size = new System.Drawing.Size(56, 24);
            this.lbMinBPMDesc.TabIndex = 10;
            this.lbMinBPMDesc.Text = "Min:";
            this.lbMinBPMDesc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbMinBPMDesc.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbMinBPMDesc.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbMinBPMDesc.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbMinBPMDesc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbBPM
            // 
            this.lbBPM.Font = new System.Drawing.Font("Calibri", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBPM.ForeColor = System.Drawing.Color.Lime;
            this.lbBPM.Location = new System.Drawing.Point(2, 10);
            this.lbBPM.Margin = new System.Windows.Forms.Padding(3);
            this.lbBPM.Name = "lbBPM";
            this.lbBPM.Size = new System.Drawing.Size(165, 93);
            this.lbBPM.TabIndex = 4;
            this.lbBPM.Text = "-";
            this.lbBPM.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbBPM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbBPM.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbBPM.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbBPM.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbBPM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Black;
            this.pnlRight.Controls.Add(this.lbBPMDesc);
            this.pnlRight.Controls.Add(this.lbBPM);
            this.pnlRight.Controls.Add(this.lbAlarm);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(425, 2);
            this.pnlRight.MaximumSize = new System.Drawing.Size(165, 146);
            this.pnlRight.MinimumSize = new System.Drawing.Size(165, 146);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(165, 146);
            this.pnlRight.TabIndex = 4;
            this.pnlRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.pnlRight.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.pnlRight.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.pnlRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.pnlRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbAlarm
            // 
            this.lbAlarm.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlarm.ForeColor = System.Drawing.Color.Lime;
            this.lbAlarm.Location = new System.Drawing.Point(3, 101);
            this.lbAlarm.Margin = new System.Windows.Forms.Padding(3);
            this.lbAlarm.Name = "lbAlarm";
            this.lbAlarm.Size = new System.Drawing.Size(164, 24);
            this.lbAlarm.TabIndex = 9;
            this.lbAlarm.Text = "ALARM";
            this.lbAlarm.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbAlarm.Visible = false;
            // 
            // lbBPMDesc
            // 
            this.lbBPMDesc.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBPMDesc.ForeColor = System.Drawing.Color.Lime;
            this.lbBPMDesc.Location = new System.Drawing.Point(3, 101);
            this.lbBPMDesc.Margin = new System.Windows.Forms.Padding(3);
            this.lbBPMDesc.Name = "lbBPMDesc";
            this.lbBPMDesc.Size = new System.Drawing.Size(164, 24);
            this.lbBPMDesc.TabIndex = 8;
            this.lbBPMDesc.Text = "BPM";
            this.lbBPMDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbBPMDesc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbBPMDesc.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbBPMDesc.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbBPMDesc.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbBPMDesc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // lbNickname
            // 
            this.lbNickname.BackColor = System.Drawing.Color.Black;
            this.lbNickname.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold);
            this.lbNickname.ForeColor = System.Drawing.Color.Lime;
            this.lbNickname.Location = new System.Drawing.Point(33, 4);
            this.lbNickname.Margin = new System.Windows.Forms.Padding(3);
            this.lbNickname.Name = "lbNickname";
            this.lbNickname.Size = new System.Drawing.Size(194, 28);
            this.lbNickname.TabIndex = 34;
            this.lbNickname.Text = "NICKNAME";
            this.lbNickname.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.lbNickname.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.lbNickname.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.lbNickname.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.lbNickname.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Lime;
            this.pnlLeft.Controls.Add(this.pnlSeparator);
            this.pnlLeft.Controls.Add(this.pnlTop);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(2, 2);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(423, 146);
            this.pnlLeft.TabIndex = 35;
            // 
            // pnlSeparator
            // 
            this.pnlSeparator.BackColor = System.Drawing.Color.Lime;
            this.pnlSeparator.Controls.Add(this.pbECGPlot);
            this.pnlSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSeparator.Location = new System.Drawing.Point(0, 40);
            this.pnlSeparator.Name = "pnlSeparator";
            this.pnlSeparator.Padding = new System.Windows.Forms.Padding(0, 2, 2, 0);
            this.pnlSeparator.Size = new System.Drawing.Size(423, 106);
            this.pnlSeparator.TabIndex = 36;
            this.pnlSeparator.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.pnlSeparator.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.pnlSeparator.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.pnlSeparator.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.pnlSeparator.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Black;
            this.pnlTop.Controls.Add(this.pbHeartBeat);
            this.pnlTop.Controls.Add(this.lbMinBPMDesc);
            this.pnlTop.Controls.Add(this.lbMaxBPM);
            this.pnlTop.Controls.Add(this.lbMaxBPMDesc);
            this.pnlTop.Controls.Add(this.lbMinBPM);
            this.pnlTop.Controls.Add(this.lbNickname);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(423, 40);
            this.pnlTop.TabIndex = 35;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.route_MouseDown);
            this.pnlTop.MouseEnter += new System.EventHandler(this.route_MouseEnter);
            this.pnlTop.MouseLeave += new System.EventHandler(this.route_MouseLeave);
            this.pnlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.route_MouseMove);
            this.pnlTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.route_MouseUp);
            // 
            // ECGDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.MaximumSize = new System.Drawing.Size(0, 150);
            this.MinimumSize = new System.Drawing.Size(592, 150);
            this.Name = "ECGDisplay";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Size = new System.Drawing.Size(592, 150);
            ((System.ComponentModel.ISupportInitialize)(this.pbECGPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeartBeat)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlSeparator.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbMaxBPM;
        private System.Windows.Forms.Label lbMinBPM;
        private System.Windows.Forms.Label lbMaxBPMDesc;
        private System.Windows.Forms.PictureBox pbECGPlot;
        private System.Windows.Forms.PictureBox pbHeartBeat;
        private System.Windows.Forms.Label lbMinBPMDesc;
        private System.Windows.Forms.Label lbBPM;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lbBPMDesc;
        private System.Windows.Forms.Label lbNickname;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlSeparator;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lbAlarm;
    }
}
