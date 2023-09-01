namespace MGT.Cardia
{
    partial class CardiaFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardiaFrm));
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.lbColor = new System.Windows.Forms.Label();
            this.lbChartTime = new System.Windows.Forms.Label();
            this.nudChartTime = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.shrinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unshrinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssBottom = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.screenColorPicker1 = new MGT.Cardia.ScreenColorPicker();
            this.lbVolume = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TrackBar();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.flpClients = new System.Windows.Forms.FlowLayoutPanel();
            this.ecgDisplay = new MGT.Cardia.ECGDisplay();
            this.msTop = new System.Windows.Forms.MenuStrip();
            this.miDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutostart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStartShrinked = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miDeviceConfigure = new System.Windows.Forms.ToolStripMenuItem();
            this.alarmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSoundPlayBeat = new System.Windows.Forms.ToolStripMenuItem();
            this.miSoundPlayAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.nudChartTime)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.ssBottom.SuspendLayout();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
            this.flpClients.SuspendLayout();
            this.msTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbColor
            // 
            this.cbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(42, 1);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(64, 21);
            this.cbColor.TabIndex = 4;
            this.cbColor.SelectedIndexChanged += new System.EventHandler(this.cbColor_SelectedIndexChanged);
            // 
            // lbColor
            // 
            this.lbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbColor.AutoSize = true;
            this.lbColor.Location = new System.Drawing.Point(2, 5);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(34, 13);
            this.lbColor.TabIndex = 5;
            this.lbColor.Text = "Color:";
            // 
            // lbChartTime
            // 
            this.lbChartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbChartTime.AutoSize = true;
            this.lbChartTime.Location = new System.Drawing.Point(120, 5);
            this.lbChartTime.Name = "lbChartTime";
            this.lbChartTime.Size = new System.Drawing.Size(57, 13);
            this.lbChartTime.TabIndex = 11;
            this.lbChartTime.Text = "Chart time:";
            // 
            // nudChartTime
            // 
            this.nudChartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudChartTime.Location = new System.Drawing.Point(183, 2);
            this.nudChartTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudChartTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudChartTime.Name = "nudChartTime";
            this.nudChartTime.Size = new System.Drawing.Size(64, 20);
            this.nudChartTime.TabIndex = 31;
            this.nudChartTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudChartTime.ValueChanged += new System.EventHandler(this.nudChartTime_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shrinkToolStripMenuItem,
            this.unshrinkToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(122, 48);
            // 
            // shrinkToolStripMenuItem
            // 
            this.shrinkToolStripMenuItem.Name = "shrinkToolStripMenuItem";
            this.shrinkToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.shrinkToolStripMenuItem.Text = "Shrink";
            this.shrinkToolStripMenuItem.Click += new System.EventHandler(this.shrinkToolStripMenuItem_Click);
            // 
            // unshrinkToolStripMenuItem
            // 
            this.unshrinkToolStripMenuItem.Name = "unshrinkToolStripMenuItem";
            this.unshrinkToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.unshrinkToolStripMenuItem.Text = "Unshrink";
            this.unshrinkToolStripMenuItem.Visible = false;
            this.unshrinkToolStripMenuItem.Click += new System.EventHandler(this.unshrinkToolStripMenuItem_Click);
            // 
            // ssBottom
            // 
            this.ssBottom.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ssBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
            this.ssBottom.Location = new System.Drawing.Point(0, 206);
            this.ssBottom.Name = "ssBottom";
            this.ssBottom.Size = new System.Drawing.Size(590, 22);
            this.ssBottom.SizingGrip = false;
            this.ssBottom.TabIndex = 32;
            this.ssBottom.Text = "ssBottom";
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.screenColorPicker1);
            this.pnlControl.Controls.Add(this.lbVolume);
            this.pnlControl.Controls.Add(this.tbVolume);
            this.pnlControl.Controls.Add(this.btnStartStop);
            this.pnlControl.Controls.Add(this.lbColor);
            this.pnlControl.Controls.Add(this.cbColor);
            this.pnlControl.Controls.Add(this.nudChartTime);
            this.pnlControl.Controls.Add(this.lbChartTime);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 176);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(590, 30);
            this.pnlControl.TabIndex = 34;
            // 
            // screenColorPicker1
            // 
            this.screenColorPicker1.Color = System.Drawing.Color.Empty;
            this.screenColorPicker1.Location = new System.Drawing.Point(431, 3);
            this.screenColorPicker1.Name = "screenColorPicker1";
            this.screenColorPicker1.Size = new System.Drawing.Size(40, 25);
            this.screenColorPicker1.Text = "screenColorPicker1";
            // 
            // lbVolume
            // 
            this.lbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbVolume.AutoSize = true;
            this.lbVolume.Location = new System.Drawing.Point(266, 5);
            this.lbVolume.Name = "lbVolume";
            this.lbVolume.Size = new System.Drawing.Size(45, 13);
            this.lbVolume.TabIndex = 39;
            this.lbVolume.Text = "Volume:";
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(311, 4);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(104, 45);
            this.tbVolume.TabIndex = 38;
            this.tbVolume.Value = 5;
            this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.Location = new System.Drawing.Point(523, -1);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(64, 23);
            this.btnStartStop.TabIndex = 37;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // flpClients
            // 
            this.flpClients.BackColor = System.Drawing.Color.Black;
            this.flpClients.Controls.Add(this.ecgDisplay);
            this.flpClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpClients.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpClients.Location = new System.Drawing.Point(0, 24);
            this.flpClients.Margin = new System.Windows.Forms.Padding(0);
            this.flpClients.Name = "flpClients";
            this.flpClients.Size = new System.Drawing.Size(590, 152);
            this.flpClients.TabIndex = 36;
            this.flpClients.Resize += new System.EventHandler(this.flpClients_Resize);
            // 
            // ecgDisplay
            // 
            this.ecgDisplay.Alarm = false;
            this.ecgDisplay.BackColor = System.Drawing.Color.Lime;
            this.ecgDisplay.Beat = false;
            this.ecgDisplay.BPM = null;
            this.ecgDisplay.BrushSize = 8;
            this.ecgDisplay.ChartTime = 4000;
            this.ecgDisplay.Color = System.Drawing.Color.Lime;
            this.ecgDisplay.Interval = 20;
            this.ecgDisplay.Location = new System.Drawing.Point(0, 0);
            this.ecgDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.ecgDisplay.MaxBPM = null;
            this.ecgDisplay.MaximumSize = new System.Drawing.Size(0, 150);
            this.ecgDisplay.MinBPM = null;
            this.ecgDisplay.MinimumSize = new System.Drawing.Size(592, 150);
            this.ecgDisplay.Name = "ecgDisplay";
            this.ecgDisplay.Nickname = null;
            this.ecgDisplay.Offset = 20;
            this.ecgDisplay.Padding = new System.Windows.Forms.Padding(2);
            this.ecgDisplay.ShowNickname = false;
            this.ecgDisplay.Size = new System.Drawing.Size(592, 150);
            this.ecgDisplay.TabIndex = 38;
            this.ecgDisplay.MouseEnter += new System.EventHandler(this.ecgDisplay_MouseEnter);
            this.ecgDisplay.MouseLeave += new System.EventHandler(this.ecgDisplay_MouseLeave);
            // 
            // msTop
            // 
            this.msTop.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.msTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miDevice,
            this.alarmToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.logToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.msTop.Location = new System.Drawing.Point(0, 0);
            this.msTop.Name = "msTop";
            this.msTop.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.msTop.Size = new System.Drawing.Size(590, 24);
            this.msTop.TabIndex = 37;
            // 
            // miDevice
            // 
            this.miDevice.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAutostart,
            this.toolStripMenuItemStartShrinked,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.miDeviceConfigure});
            this.miDevice.Name = "miDevice";
            this.miDevice.Size = new System.Drawing.Size(54, 22);
            this.miDevice.Text = "Device";
            // 
            // toolStripMenuItemAutostart
            // 
            this.toolStripMenuItemAutostart.CheckOnClick = true;
            this.toolStripMenuItemAutostart.Name = "toolStripMenuItemAutostart";
            this.toolStripMenuItemAutostart.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemAutostart.Text = "Autostart";
            this.toolStripMenuItemAutostart.CheckedChanged += new System.EventHandler(this.toolStripMenuItemAutostart_CheckedChanged);
            // 
            // toolStripMenuItemStartShrinked
            // 
            this.toolStripMenuItemStartShrinked.CheckOnClick = true;
            this.toolStripMenuItemStartShrinked.Name = "toolStripMenuItemStartShrinked";
            this.toolStripMenuItemStartShrinked.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItemStartShrinked.Text = "Start shrinked";
            this.toolStripMenuItemStartShrinked.CheckedChanged += new System.EventHandler(this.toolStripMenuItemStartShrinked_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // miDeviceConfigure
            // 
            this.miDeviceConfigure.Name = "miDeviceConfigure";
            this.miDeviceConfigure.Size = new System.Drawing.Size(148, 22);
            this.miDeviceConfigure.Text = "Configuration";
            this.miDeviceConfigure.Click += new System.EventHandler(this.miDeviceConfigure_Click);
            // 
            // alarmToolStripMenuItem
            // 
            this.alarmToolStripMenuItem.Name = "alarmToolStripMenuItem";
            this.alarmToolStripMenuItem.Size = new System.Drawing.Size(51, 22);
            this.alarmToolStripMenuItem.Text = "Alarm";
            this.alarmToolStripMenuItem.Click += new System.EventHandler(this.alarmToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSoundPlayBeat,
            this.miSoundPlayAlarm});
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(53, 22);
            this.soundToolStripMenuItem.Text = "Sound";
            // 
            // miSoundPlayBeat
            // 
            this.miSoundPlayBeat.Name = "miSoundPlayBeat";
            this.miSoundPlayBeat.Size = new System.Drawing.Size(129, 22);
            this.miSoundPlayBeat.Text = "Play beat";
            this.miSoundPlayBeat.Click += new System.EventHandler(this.miSoundPlayBeat_Click);
            // 
            // miSoundPlayAlarm
            // 
            this.miSoundPlayAlarm.Name = "miSoundPlayAlarm";
            this.miSoundPlayAlarm.Size = new System.Drawing.Size(129, 22);
            this.miSoundPlayAlarm.Text = "Play alarm";
            this.miSoundPlayAlarm.Click += new System.EventHandler(this.miSoundPlayAlarm_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(39, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.miLog_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(64, 22);
            this.networkToolStripMenuItem.Text = "Network";
            this.networkToolStripMenuItem.Click += new System.EventHandler(this.miNetwork_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // abcToolStripMenuItem
            // 
            this.abcToolStripMenuItem.Name = "abcToolStripMenuItem";
            this.abcToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // CardiaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 228);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.flpClients);
            this.Controls.Add(this.pnlControl);
            this.Controls.Add(this.ssBottom);
            this.Controls.Add(this.msTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msTop;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(4094, 267);
            this.MinimumSize = new System.Drawing.Size(606, 267);
            this.Name = "CardiaFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cardia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HRMUISmall_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Main_Paint);
            this.Move += new System.EventHandler(this.Main_Move);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nudChartTime)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ssBottom.ResumeLayout(false);
            this.ssBottom.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
            this.flpClients.ResumeLayout(false);
            this.msTop.ResumeLayout(false);
            this.msTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.Label lbColor;
        private System.Windows.Forms.Label lbChartTime;
        private System.Windows.Forms.NumericUpDown nudChartTime;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem shrinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unshrinkToolStripMenuItem;
        private System.Windows.Forms.StatusStrip ssBottom;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.FlowLayoutPanel flpClients;
        private ECGDisplay ecgDisplay;
        private System.Windows.Forms.MenuStrip msTop;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miDevice;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miDeviceConfigure;
        private System.Windows.Forms.TrackBar tbVolume;
        private System.Windows.Forms.Label lbVolume;
        private System.Windows.Forms.ToolStripMenuItem abcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSoundPlayBeat;
        private System.Windows.Forms.ToolStripMenuItem miSoundPlayAlarm;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutostart;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStartShrinked;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private ScreenColorPicker screenColorPicker1;
    }
}