namespace MGT.Cardia
{
    partial class AlarmFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmFrm));
            this.lbAlarmEnable = new System.Windows.Forms.Label();
            this.cbAlarmEnable = new System.Windows.Forms.CheckBox();
            this.lbAlarmDefuse = new System.Windows.Forms.Label();
            this.cbAlarmDefuse = new System.Windows.Forms.CheckBox();
            this.nudAlarmDefuseTime = new System.Windows.Forms.NumericUpDown();
            this.nudAlarmLowThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudAlarmHighThreshold = new System.Windows.Forms.NumericUpDown();
            this.lbAlarmLowThreshold = new System.Windows.Forms.Label();
            this.lbAlarmHighThreshold = new System.Windows.Forms.Label();
            this.lbAlarmDefuseTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmDefuseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmLowThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmHighThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // lbAlarmEnable
            // 
            this.lbAlarmEnable.Location = new System.Drawing.Point(12, 9);
            this.lbAlarmEnable.Name = "lbAlarmEnable";
            this.lbAlarmEnable.Size = new System.Drawing.Size(81, 23);
            this.lbAlarmEnable.TabIndex = 26;
            this.lbAlarmEnable.Text = "Enable alarm:";
            this.lbAlarmEnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAlarmEnable
            // 
            this.cbAlarmEnable.AutoSize = true;
            this.cbAlarmEnable.Location = new System.Drawing.Point(99, 14);
            this.cbAlarmEnable.Name = "cbAlarmEnable";
            this.cbAlarmEnable.Size = new System.Drawing.Size(15, 14);
            this.cbAlarmEnable.TabIndex = 25;
            this.cbAlarmEnable.UseVisualStyleBackColor = true;
            this.cbAlarmEnable.CheckedChanged += new System.EventHandler(this.cbAlarmEnable_CheckedChanged);
            // 
            // lbAlarmDefuse
            // 
            this.lbAlarmDefuse.Location = new System.Drawing.Point(185, 9);
            this.lbAlarmDefuse.Name = "lbAlarmDefuse";
            this.lbAlarmDefuse.Size = new System.Drawing.Size(81, 23);
            this.lbAlarmDefuse.TabIndex = 27;
            this.lbAlarmDefuse.Text = "Defuse alarm:";
            this.lbAlarmDefuse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAlarmDefuse
            // 
            this.cbAlarmDefuse.AutoSize = true;
            this.cbAlarmDefuse.Location = new System.Drawing.Point(277, 14);
            this.cbAlarmDefuse.Name = "cbAlarmDefuse";
            this.cbAlarmDefuse.Size = new System.Drawing.Size(15, 14);
            this.cbAlarmDefuse.TabIndex = 28;
            this.cbAlarmDefuse.UseVisualStyleBackColor = true;
            this.cbAlarmDefuse.CheckedChanged += new System.EventHandler(this.cbAlarmDefuse_CheckedChanged);
            // 
            // nudAlarmDefuseTime
            // 
            this.nudAlarmDefuseTime.Location = new System.Drawing.Point(277, 38);
            this.nudAlarmDefuseTime.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nudAlarmDefuseTime.Name = "nudAlarmDefuseTime";
            this.nudAlarmDefuseTime.Size = new System.Drawing.Size(64, 20);
            this.nudAlarmDefuseTime.TabIndex = 29;
            this.nudAlarmDefuseTime.ValueChanged += new System.EventHandler(this.nudAlarmDefuseTime_ValueChanged);
            // 
            // nudAlarmLowThreshold
            // 
            this.nudAlarmLowThreshold.Location = new System.Drawing.Point(99, 38);
            this.nudAlarmLowThreshold.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudAlarmLowThreshold.Name = "nudAlarmLowThreshold";
            this.nudAlarmLowThreshold.Size = new System.Drawing.Size(64, 20);
            this.nudAlarmLowThreshold.TabIndex = 30;
            this.nudAlarmLowThreshold.ValueChanged += new System.EventHandler(this.nudAlarmLowThreshold_ValueChanged);
            // 
            // nudAlarmHighThreshold
            // 
            this.nudAlarmHighThreshold.Location = new System.Drawing.Point(99, 64);
            this.nudAlarmHighThreshold.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudAlarmHighThreshold.Name = "nudAlarmHighThreshold";
            this.nudAlarmHighThreshold.Size = new System.Drawing.Size(64, 20);
            this.nudAlarmHighThreshold.TabIndex = 31;
            this.nudAlarmHighThreshold.ValueChanged += new System.EventHandler(this.nudAlarmHighThreshold_ValueChanged);
            // 
            // lbAlarmLowThreshold
            // 
            this.lbAlarmLowThreshold.Location = new System.Drawing.Point(12, 35);
            this.lbAlarmLowThreshold.Name = "lbAlarmLowThreshold";
            this.lbAlarmLowThreshold.Size = new System.Drawing.Size(81, 23);
            this.lbAlarmLowThreshold.TabIndex = 32;
            this.lbAlarmLowThreshold.Text = "Low threshold:";
            this.lbAlarmLowThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAlarmHighThreshold
            // 
            this.lbAlarmHighThreshold.Location = new System.Drawing.Point(12, 61);
            this.lbAlarmHighThreshold.Name = "lbAlarmHighThreshold";
            this.lbAlarmHighThreshold.Size = new System.Drawing.Size(81, 23);
            this.lbAlarmHighThreshold.TabIndex = 33;
            this.lbAlarmHighThreshold.Text = "High threshold:";
            this.lbAlarmHighThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAlarmDefuseTime
            // 
            this.lbAlarmDefuseTime.Location = new System.Drawing.Point(185, 35);
            this.lbAlarmDefuseTime.Name = "lbAlarmDefuseTime";
            this.lbAlarmDefuseTime.Size = new System.Drawing.Size(81, 23);
            this.lbAlarmDefuseTime.TabIndex = 34;
            this.lbAlarmDefuseTime.Text = "Defuse time:";
            this.lbAlarmDefuseTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AlarmFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 100);
            this.Controls.Add(this.lbAlarmDefuseTime);
            this.Controls.Add(this.lbAlarmHighThreshold);
            this.Controls.Add(this.lbAlarmLowThreshold);
            this.Controls.Add(this.nudAlarmHighThreshold);
            this.Controls.Add(this.nudAlarmLowThreshold);
            this.Controls.Add(this.nudAlarmDefuseTime);
            this.Controls.Add(this.cbAlarmDefuse);
            this.Controls.Add(this.lbAlarmDefuse);
            this.Controls.Add(this.lbAlarmEnable);
            this.Controls.Add(this.cbAlarmEnable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlarmFrm";
            this.ShowInTaskbar = false;
            this.Text = "Alarm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlarmFrm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmDefuseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmLowThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlarmHighThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAlarmEnable;
        private System.Windows.Forms.CheckBox cbAlarmEnable;
        private System.Windows.Forms.Label lbAlarmDefuse;
        private System.Windows.Forms.CheckBox cbAlarmDefuse;
        private System.Windows.Forms.NumericUpDown nudAlarmDefuseTime;
        private System.Windows.Forms.NumericUpDown nudAlarmLowThreshold;
        private System.Windows.Forms.NumericUpDown nudAlarmHighThreshold;
        private System.Windows.Forms.Label lbAlarmLowThreshold;
        private System.Windows.Forms.Label lbAlarmHighThreshold;
        private System.Windows.Forms.Label lbAlarmDefuseTime;
    }
}