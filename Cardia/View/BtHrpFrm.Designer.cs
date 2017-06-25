namespace MGT.Cardia
{
    partial class BtHrpFrm
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
            System.Windows.Forms.Label label21;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BtHrpFrm));
            this.gbDeviceStatus = new System.Windows.Forms.GroupBox();
            this.tbHeartRate = new System.Windows.Forms.TextBox();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.nudCharacteristic = new System.Windows.Forms.NumericUpDown();
            this.lbCharacteristic = new System.Windows.Forms.Label();
            this.cbDevices = new System.Windows.Forms.ComboBox();
            this.lbDevices = new System.Windows.Forms.Label();
            this.nudInitDelay = new System.Windows.Forms.NumericUpDown();
            this.lbInitDelay = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            this.gbDeviceStatus.SuspendLayout();
            this.gbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharacteristic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitDelay)).BeginInit();
            this.SuspendLayout();
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
            // gbDeviceStatus
            // 
            this.gbDeviceStatus.Controls.Add(label21);
            this.gbDeviceStatus.Controls.Add(this.tbHeartRate);
            this.gbDeviceStatus.Location = new System.Drawing.Point(9, 116);
            this.gbDeviceStatus.Name = "gbDeviceStatus";
            this.gbDeviceStatus.Size = new System.Drawing.Size(341, 51);
            this.gbDeviceStatus.TabIndex = 96;
            this.gbDeviceStatus.TabStop = false;
            this.gbDeviceStatus.Text = "Device Status";
            // 
            // tbHeartRate
            // 
            this.tbHeartRate.Location = new System.Drawing.Point(93, 19);
            this.tbHeartRate.Name = "tbHeartRate";
            this.tbHeartRate.ReadOnly = true;
            this.tbHeartRate.Size = new System.Drawing.Size(236, 20);
            this.tbHeartRate.TabIndex = 59;
            this.tbHeartRate.TabStop = false;
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.lbInitDelay);
            this.gbConfiguration.Controls.Add(this.nudInitDelay);
            this.gbConfiguration.Controls.Add(this.nudCharacteristic);
            this.gbConfiguration.Controls.Add(this.lbCharacteristic);
            this.gbConfiguration.Controls.Add(this.cbDevices);
            this.gbConfiguration.Controls.Add(this.lbDevices);
            this.gbConfiguration.Location = new System.Drawing.Point(9, 9);
            this.gbConfiguration.Margin = new System.Windows.Forms.Padding(0);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(341, 104);
            this.gbConfiguration.TabIndex = 95;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration";
            // 
            // nudCharacteristic
            // 
            this.nudCharacteristic.Location = new System.Drawing.Point(93, 45);
            this.nudCharacteristic.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudCharacteristic.Name = "nudCharacteristic";
            this.nudCharacteristic.Size = new System.Drawing.Size(236, 20);
            this.nudCharacteristic.TabIndex = 13;
            this.nudCharacteristic.ValueChanged += new System.EventHandler(this.nudCharacteristic_ValueChanged);
            // 
            // lbCharacteristic
            // 
            this.lbCharacteristic.Location = new System.Drawing.Point(6, 42);
            this.lbCharacteristic.Name = "lbCharacteristic";
            this.lbCharacteristic.Size = new System.Drawing.Size(81, 23);
            this.lbCharacteristic.TabIndex = 12;
            this.lbCharacteristic.Text = "Characteristic:";
            this.lbCharacteristic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDevices
            // 
            this.cbDevices.DisplayMember = "Name";
            this.cbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevices.FormattingEnabled = true;
            this.cbDevices.Location = new System.Drawing.Point(93, 18);
            this.cbDevices.Name = "cbDevices";
            this.cbDevices.Size = new System.Drawing.Size(236, 21);
            this.cbDevices.TabIndex = 10;
            this.cbDevices.SelectedIndexChanged += new System.EventHandler(this.cbDevices_SelectedIndexChanged);
            // 
            // lbDevices
            // 
            this.lbDevices.Location = new System.Drawing.Point(6, 16);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(81, 23);
            this.lbDevices.TabIndex = 11;
            this.lbDevices.Text = "Device:";
            this.lbDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudInitDelay
            // 
            this.nudInitDelay.Location = new System.Drawing.Point(93, 71);
            this.nudInitDelay.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudInitDelay.Name = "nudInitDelay";
            this.nudInitDelay.Size = new System.Drawing.Size(236, 20);
            this.nudInitDelay.TabIndex = 14;
            this.nudInitDelay.ValueChanged += new System.EventHandler(this.nudInitDelay_ValueChanged);
            // 
            // lbInitDelay
            // 
            this.lbInitDelay.Location = new System.Drawing.Point(6, 68);
            this.lbInitDelay.Name = "lbInitDelay";
            this.lbInitDelay.Size = new System.Drawing.Size(81, 23);
            this.lbInitDelay.TabIndex = 15;
            this.lbInitDelay.Text = "Initi delay:";
            this.lbInitDelay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtHrpFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 177);
            this.Controls.Add(this.gbDeviceStatus);
            this.Controls.Add(this.gbConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BtHrpFrm";
            this.ShowInTaskbar = false;
            this.Text = "Bluetooth Smart HRP";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BtHrpFrm_FormClosing);
            this.gbDeviceStatus.ResumeLayout(false);
            this.gbDeviceStatus.PerformLayout();
            this.gbConfiguration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCharacteristic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudInitDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDeviceStatus;
        private System.Windows.Forms.TextBox tbHeartRate;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.ComboBox cbDevices;
        private System.Windows.Forms.Label lbDevices;
        private System.Windows.Forms.NumericUpDown nudCharacteristic;
        private System.Windows.Forms.Label lbCharacteristic;
        private System.Windows.Forms.Label lbInitDelay;
        private System.Windows.Forms.NumericUpDown nudInitDelay;

    }
}