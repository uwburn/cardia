namespace MGT.Cardia
{
    partial class HRMEmulatorFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HRMEmulatorFrm));
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.nudMaxBPM = new System.Windows.Forms.NumericUpDown();
            this.nudMinBPM = new System.Windows.Forms.NumericUpDown();
            this.lbMaxBPM = new System.Windows.Forms.Label();
            this.lbMinBPM = new System.Windows.Forms.Label();
            this.gbConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinBPM)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.nudMaxBPM);
            this.gbConfiguration.Controls.Add(this.nudMinBPM);
            this.gbConfiguration.Controls.Add(this.lbMaxBPM);
            this.gbConfiguration.Controls.Add(this.lbMinBPM);
            this.gbConfiguration.Location = new System.Drawing.Point(9, 9);
            this.gbConfiguration.Margin = new System.Windows.Forms.Padding(0);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(340, 51);
            this.gbConfiguration.TabIndex = 95;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration";
            // 
            // nudMaxBPM
            // 
            this.nudMaxBPM.Location = new System.Drawing.Point(265, 18);
            this.nudMaxBPM.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudMaxBPM.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMaxBPM.Name = "nudMaxBPM";
            this.nudMaxBPM.Size = new System.Drawing.Size(64, 20);
            this.nudMaxBPM.TabIndex = 58;
            this.nudMaxBPM.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudMaxBPM.ValueChanged += new System.EventHandler(this.nudMaxBPM_ValueChanged);
            // 
            // nudMinBPM
            // 
            this.nudMinBPM.Location = new System.Drawing.Point(93, 18);
            this.nudMinBPM.Maximum = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.nudMinBPM.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMinBPM.Name = "nudMinBPM";
            this.nudMinBPM.Size = new System.Drawing.Size(64, 20);
            this.nudMinBPM.TabIndex = 57;
            this.nudMinBPM.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMinBPM.ValueChanged += new System.EventHandler(this.nudMinBPM_ValueChanged);
            // 
            // lbMaxBPM
            // 
            this.lbMaxBPM.Location = new System.Drawing.Point(178, 16);
            this.lbMaxBPM.Name = "lbMaxBPM";
            this.lbMaxBPM.Size = new System.Drawing.Size(81, 23);
            this.lbMaxBPM.TabIndex = 56;
            this.lbMaxBPM.Text = "Max BPM:";
            this.lbMaxBPM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMinBPM
            // 
            this.lbMinBPM.Location = new System.Drawing.Point(6, 16);
            this.lbMinBPM.Name = "lbMinBPM";
            this.lbMinBPM.Size = new System.Drawing.Size(81, 23);
            this.lbMinBPM.TabIndex = 11;
            this.lbMinBPM.Text = "Min BPM:";
            this.lbMinBPM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // HRM_Emulator_Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 73);
            this.Controls.Add(this.gbConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HRM_Emulator_Panel";
            this.ShowInTaskbar = false;
            this.Text = "HRM Emulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HRM_Emulator_Panel_FormClosing);
            this.gbConfiguration.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxBPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinBPM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.Label lbMinBPM;
        private System.Windows.Forms.Label lbMaxBPM;
        private System.Windows.Forms.NumericUpDown nudMaxBPM;
        private System.Windows.Forms.NumericUpDown nudMinBPM;

    }
}