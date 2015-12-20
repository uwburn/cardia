namespace MGT.Cardia
{
    partial class CMS50Frm
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
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label21;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label14;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CMS50Frm));
            this.gbDeviceStatus = new System.Windows.Forms.GroupBox();
            this.tbSpO2 = new System.Windows.Forms.TextBox();
            this.lbStrength = new System.Windows.Forms.Label();
            this.pbStrength = new System.Windows.Forms.ProgressBar();
            this.tbSpO2Drop = new System.Windows.Forms.TextBox();
            this.tbHeartbeatCount = new System.Windows.Forms.TextBox();
            this.tbHeartRate = new System.Windows.Forms.TextBox();
            this.tbBeat = new System.Windows.Forms.TextBox();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.tbBarGraph = new System.Windows.Forms.TextBox();
            this.tbWaveform = new System.Windows.Forms.TextBox();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.cbSerialPorts = new System.Windows.Forms.ComboBox();
            this.lbSerialPorts = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            this.gbDeviceStatus.SuspendLayout();
            this.gbConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            label8.Location = new System.Drawing.Point(178, 16);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(81, 23);
            label8.TabIndex = 78;
            label8.Text = "SpO2:";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            label22.Location = new System.Drawing.Point(6, 43);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(81, 23);
            label22.TabIndex = 92;
            label22.Text = "Heartbeats:";
            label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            label21.Location = new System.Drawing.Point(6, 17);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(81, 23);
            label21.TabIndex = 91;
            label21.Text = "Heart rate:";
            label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(6, 69);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 23);
            label4.TabIndex = 52;
            label4.Text = "Beat:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            label16.Location = new System.Drawing.Point(350, 42);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(81, 23);
            label16.TabIndex = 86;
            label16.Text = "Bar graph:";
            label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            label9.Location = new System.Drawing.Point(178, 42);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(81, 23);
            label9.TabIndex = 79;
            label9.Text = "SpO2 drop:";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            label15.Location = new System.Drawing.Point(350, 16);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(81, 23);
            label15.TabIndex = 85;
            label15.Text = "Waveform:";
            label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            label14.Location = new System.Drawing.Point(178, 122);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(81, 23);
            label14.TabIndex = 84;
            label14.Text = "Status:";
            label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbDeviceStatus
            // 
            this.gbDeviceStatus.Controls.Add(label8);
            this.gbDeviceStatus.Controls.Add(this.tbSpO2);
            this.gbDeviceStatus.Controls.Add(this.lbStrength);
            this.gbDeviceStatus.Controls.Add(label22);
            this.gbDeviceStatus.Controls.Add(this.pbStrength);
            this.gbDeviceStatus.Controls.Add(this.tbSpO2Drop);
            this.gbDeviceStatus.Controls.Add(label21);
            this.gbDeviceStatus.Controls.Add(this.tbHeartbeatCount);
            this.gbDeviceStatus.Controls.Add(this.tbHeartRate);
            this.gbDeviceStatus.Controls.Add(this.tbBeat);
            this.gbDeviceStatus.Controls.Add(label4);
            this.gbDeviceStatus.Controls.Add(this.tbStatus);
            this.gbDeviceStatus.Controls.Add(label16);
            this.gbDeviceStatus.Controls.Add(label9);
            this.gbDeviceStatus.Controls.Add(label15);
            this.gbDeviceStatus.Controls.Add(label14);
            this.gbDeviceStatus.Controls.Add(this.tbBarGraph);
            this.gbDeviceStatus.Controls.Add(this.tbWaveform);
            this.gbDeviceStatus.Location = new System.Drawing.Point(9, 62);
            this.gbDeviceStatus.Name = "gbDeviceStatus";
            this.gbDeviceStatus.Size = new System.Drawing.Size(512, 153);
            this.gbDeviceStatus.TabIndex = 96;
            this.gbDeviceStatus.TabStop = false;
            this.gbDeviceStatus.Text = "Device Status";
            // 
            // tbSpO2
            // 
            this.tbSpO2.Location = new System.Drawing.Point(265, 19);
            this.tbSpO2.Name = "tbSpO2";
            this.tbSpO2.ReadOnly = true;
            this.tbSpO2.Size = new System.Drawing.Size(64, 20);
            this.tbSpO2.TabIndex = 64;
            this.tbSpO2.TabStop = false;
            // 
            // lbStrength
            // 
            this.lbStrength.Location = new System.Drawing.Point(6, 122);
            this.lbStrength.Name = "lbStrength";
            this.lbStrength.Size = new System.Drawing.Size(81, 23);
            this.lbStrength.TabIndex = 16;
            this.lbStrength.Text = "Signal strength:";
            this.lbStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbStrength
            // 
            this.pbStrength.Location = new System.Drawing.Point(93, 123);
            this.pbStrength.Maximum = 1;
            this.pbStrength.Name = "pbStrength";
            this.pbStrength.Size = new System.Drawing.Size(64, 19);
            this.pbStrength.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbStrength.TabIndex = 15;
            // 
            // tbSpO2Drop
            // 
            this.tbSpO2Drop.Location = new System.Drawing.Point(265, 45);
            this.tbSpO2Drop.Name = "tbSpO2Drop";
            this.tbSpO2Drop.ReadOnly = true;
            this.tbSpO2Drop.Size = new System.Drawing.Size(64, 20);
            this.tbSpO2Drop.TabIndex = 65;
            this.tbSpO2Drop.TabStop = false;
            // 
            // tbHeartbeatCount
            // 
            this.tbHeartbeatCount.Location = new System.Drawing.Point(93, 45);
            this.tbHeartbeatCount.Name = "tbHeartbeatCount";
            this.tbHeartbeatCount.ReadOnly = true;
            this.tbHeartbeatCount.Size = new System.Drawing.Size(64, 20);
            this.tbHeartbeatCount.TabIndex = 60;
            this.tbHeartbeatCount.TabStop = false;
            // 
            // tbHeartRate
            // 
            this.tbHeartRate.Location = new System.Drawing.Point(93, 19);
            this.tbHeartRate.Name = "tbHeartRate";
            this.tbHeartRate.ReadOnly = true;
            this.tbHeartRate.Size = new System.Drawing.Size(64, 20);
            this.tbHeartRate.TabIndex = 59;
            this.tbHeartRate.TabStop = false;
            // 
            // tbBeat
            // 
            this.tbBeat.Location = new System.Drawing.Point(93, 71);
            this.tbBeat.Name = "tbBeat";
            this.tbBeat.ReadOnly = true;
            this.tbBeat.Size = new System.Drawing.Size(64, 20);
            this.tbBeat.TabIndex = 53;
            this.tbBeat.TabStop = false;
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(265, 124);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(236, 20);
            this.tbStatus.TabIndex = 70;
            this.tbStatus.TabStop = false;
            // 
            // tbBarGraph
            // 
            this.tbBarGraph.Location = new System.Drawing.Point(437, 44);
            this.tbBarGraph.Name = "tbBarGraph";
            this.tbBarGraph.ReadOnly = true;
            this.tbBarGraph.Size = new System.Drawing.Size(64, 20);
            this.tbBarGraph.TabIndex = 72;
            this.tbBarGraph.TabStop = false;
            // 
            // tbWaveform
            // 
            this.tbWaveform.Location = new System.Drawing.Point(437, 18);
            this.tbWaveform.Name = "tbWaveform";
            this.tbWaveform.ReadOnly = true;
            this.tbWaveform.Size = new System.Drawing.Size(64, 20);
            this.tbWaveform.TabIndex = 71;
            this.tbWaveform.TabStop = false;
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.cbSerialPorts);
            this.gbConfiguration.Controls.Add(this.lbSerialPorts);
            this.gbConfiguration.Location = new System.Drawing.Point(9, 9);
            this.gbConfiguration.Margin = new System.Windows.Forms.Padding(0);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(512, 50);
            this.gbConfiguration.TabIndex = 95;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration";
            // 
            // cbSerialPorts
            // 
            this.cbSerialPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerialPorts.FormattingEnabled = true;
            this.cbSerialPorts.Location = new System.Drawing.Point(93, 18);
            this.cbSerialPorts.Name = "cbSerialPorts";
            this.cbSerialPorts.Size = new System.Drawing.Size(64, 21);
            this.cbSerialPorts.TabIndex = 10;
            this.cbSerialPorts.SelectedIndexChanged += new System.EventHandler(this.cbSerialPorts_SelectedIndexChanged);
            // 
            // lbSerialPorts
            // 
            this.lbSerialPorts.Location = new System.Drawing.Point(6, 16);
            this.lbSerialPorts.Name = "lbSerialPorts";
            this.lbSerialPorts.Size = new System.Drawing.Size(81, 23);
            this.lbSerialPorts.TabIndex = 11;
            this.lbSerialPorts.Text = "Serial port:";
            this.lbSerialPorts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CMS50Frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 225);
            this.Controls.Add(this.gbDeviceStatus);
            this.Controls.Add(this.gbConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CMS50Frm";
            this.ShowInTaskbar = false;
            this.Text = "CMS50";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CMS50Frm_FormClosing);
            this.gbDeviceStatus.ResumeLayout(false);
            this.gbDeviceStatus.PerformLayout();
            this.gbConfiguration.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDeviceStatus;
        private System.Windows.Forms.TextBox tbSpO2;
        private System.Windows.Forms.Label lbStrength;
        private System.Windows.Forms.ProgressBar pbStrength;
        private System.Windows.Forms.TextBox tbSpO2Drop;
        private System.Windows.Forms.TextBox tbHeartbeatCount;
        private System.Windows.Forms.TextBox tbHeartRate;
        private System.Windows.Forms.TextBox tbBeat;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.TextBox tbBarGraph;
        private System.Windows.Forms.TextBox tbWaveform;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.ComboBox cbSerialPorts;
        private System.Windows.Forms.Label lbSerialPorts;

    }
}