namespace MGT.Cardia
{
    partial class LogFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogFrm));
            this.btnLogDestination = new System.Windows.Forms.Button();
            this.lbLogFile = new System.Windows.Forms.Label();
            this.tbLogFile = new System.Windows.Forms.TextBox();
            this.lbLogEnable = new System.Windows.Forms.Label();
            this.cbLogEnable = new System.Windows.Forms.CheckBox();
            this.rbLogCSV = new System.Windows.Forms.RadioButton();
            this.rbLogXLSX = new System.Windows.Forms.RadioButton();
            this.rbLogXML = new System.Windows.Forms.RadioButton();
            this.lbLogFormat = new System.Windows.Forms.Label();
            this.sfdDestination = new System.Windows.Forms.SaveFileDialog();
            this.lbIpAddress = new System.Windows.Forms.Label();
            this.tbIpAddress = new System.Windows.Forms.TextBox();
            this.nudPort = new System.Windows.Forms.NumericUpDown();
            this.lbPort = new System.Windows.Forms.Label();
            this.rbLogUDP = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogDestination
            // 
            this.btnLogDestination.Location = new System.Drawing.Point(319, 118);
            this.btnLogDestination.Name = "btnLogDestination";
            this.btnLogDestination.Size = new System.Drawing.Size(27, 23);
            this.btnLogDestination.TabIndex = 27;
            this.btnLogDestination.Text = "...";
            this.btnLogDestination.UseVisualStyleBackColor = true;
            this.btnLogDestination.Click += new System.EventHandler(this.btnLogDestination_Click);
            // 
            // lbLogFile
            // 
            this.lbLogFile.Location = new System.Drawing.Point(12, 118);
            this.lbLogFile.Name = "lbLogFile";
            this.lbLogFile.Size = new System.Drawing.Size(81, 23);
            this.lbLogFile.TabIndex = 26;
            this.lbLogFile.Text = "File:";
            this.lbLogFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbLogFile
            // 
            this.tbLogFile.Location = new System.Drawing.Point(99, 120);
            this.tbLogFile.Name = "tbLogFile";
            this.tbLogFile.Size = new System.Drawing.Size(214, 20);
            this.tbLogFile.TabIndex = 25;
            this.tbLogFile.TextChanged += new System.EventHandler(this.tbLogDestination_TextChanged);
            // 
            // lbLogEnable
            // 
            this.lbLogEnable.Location = new System.Drawing.Point(12, 9);
            this.lbLogEnable.Name = "lbLogEnable";
            this.lbLogEnable.Size = new System.Drawing.Size(81, 23);
            this.lbLogEnable.TabIndex = 24;
            this.lbLogEnable.Text = "Enable log:";
            this.lbLogEnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbLogEnable
            // 
            this.cbLogEnable.AutoSize = true;
            this.cbLogEnable.Location = new System.Drawing.Point(99, 14);
            this.cbLogEnable.Name = "cbLogEnable";
            this.cbLogEnable.Size = new System.Drawing.Size(15, 14);
            this.cbLogEnable.TabIndex = 23;
            this.cbLogEnable.UseVisualStyleBackColor = true;
            this.cbLogEnable.CheckedChanged += new System.EventHandler(this.cbLogEnable_CheckedChanged);
            // 
            // rbLogCSV
            // 
            this.rbLogCSV.AutoSize = true;
            this.rbLogCSV.Checked = true;
            this.rbLogCSV.Location = new System.Drawing.Point(272, 12);
            this.rbLogCSV.Name = "rbLogCSV";
            this.rbLogCSV.Size = new System.Drawing.Size(46, 17);
            this.rbLogCSV.TabIndex = 22;
            this.rbLogCSV.TabStop = true;
            this.rbLogCSV.Text = "CSV";
            this.rbLogCSV.UseVisualStyleBackColor = true;
            this.rbLogCSV.CheckedChanged += new System.EventHandler(this.rbLogCSV_CheckedChanged);
            // 
            // rbLogXLSX
            // 
            this.rbLogXLSX.AutoSize = true;
            this.rbLogXLSX.Location = new System.Drawing.Point(272, 35);
            this.rbLogXLSX.Name = "rbLogXLSX";
            this.rbLogXLSX.Size = new System.Drawing.Size(52, 17);
            this.rbLogXLSX.TabIndex = 21;
            this.rbLogXLSX.Text = "XLSX";
            this.rbLogXLSX.UseVisualStyleBackColor = true;
            this.rbLogXLSX.CheckedChanged += new System.EventHandler(this.rbLogXLSX_CheckedChanged);
            // 
            // rbLogXML
            // 
            this.rbLogXML.AutoSize = true;
            this.rbLogXML.Location = new System.Drawing.Point(272, 58);
            this.rbLogXML.Name = "rbLogXML";
            this.rbLogXML.Size = new System.Drawing.Size(47, 17);
            this.rbLogXML.TabIndex = 20;
            this.rbLogXML.Text = "XML";
            this.rbLogXML.UseVisualStyleBackColor = true;
            this.rbLogXML.CheckedChanged += new System.EventHandler(this.rbLogXML_CheckedChanged);
            // 
            // lbLogFormat
            // 
            this.lbLogFormat.Location = new System.Drawing.Point(185, 9);
            this.lbLogFormat.Name = "lbLogFormat";
            this.lbLogFormat.Size = new System.Drawing.Size(81, 23);
            this.lbLogFormat.TabIndex = 19;
            this.lbLogFormat.Text = "Log format:";
            this.lbLogFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sfdDestination
            // 
            this.sfdDestination.FileOk += new System.ComponentModel.CancelEventHandler(this.sfdDestination_FileOk);
            // 
            // lbIpAddress
            // 
            this.lbIpAddress.Location = new System.Drawing.Point(12, 144);
            this.lbIpAddress.Name = "lbIpAddress";
            this.lbIpAddress.Size = new System.Drawing.Size(81, 23);
            this.lbIpAddress.TabIndex = 29;
            this.lbIpAddress.Text = "IP address:";
            this.lbIpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbIpAddress
            // 
            this.tbIpAddress.Location = new System.Drawing.Point(99, 146);
            this.tbIpAddress.Name = "tbIpAddress";
            this.tbIpAddress.Size = new System.Drawing.Size(247, 20);
            this.tbIpAddress.TabIndex = 28;
            this.tbIpAddress.TextChanged += new System.EventHandler(this.tbIpAddress_TextChanged);
            // 
            // nudPort
            // 
            this.nudPort.Location = new System.Drawing.Point(99, 172);
            this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPort.Name = "nudPort";
            this.nudPort.Size = new System.Drawing.Size(247, 20);
            this.nudPort.TabIndex = 30;
            this.nudPort.Value = new decimal(new int[] {
            60900,
            0,
            0,
            0});
            this.nudPort.ValueChanged += new System.EventHandler(this.nudPort_ValueChanged);
            // 
            // lbPort
            // 
            this.lbPort.Location = new System.Drawing.Point(12, 172);
            this.lbPort.Name = "lbPort";
            this.lbPort.Size = new System.Drawing.Size(81, 23);
            this.lbPort.TabIndex = 31;
            this.lbPort.Text = "Port:";
            this.lbPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbLogUDP
            // 
            this.rbLogUDP.AutoSize = true;
            this.rbLogUDP.Location = new System.Drawing.Point(272, 81);
            this.rbLogUDP.Name = "rbLogUDP";
            this.rbLogUDP.Size = new System.Drawing.Size(48, 17);
            this.rbLogUDP.TabIndex = 32;
            this.rbLogUDP.Text = "UDP";
            this.rbLogUDP.UseVisualStyleBackColor = true;
            this.rbLogUDP.CheckedChanged += new System.EventHandler(this.rbLogUDP_CheckedChanged);
            // 
            // LogFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 205);
            this.Controls.Add(this.rbLogUDP);
            this.Controls.Add(this.lbPort);
            this.Controls.Add(this.nudPort);
            this.Controls.Add(this.lbIpAddress);
            this.Controls.Add(this.tbIpAddress);
            this.Controls.Add(this.btnLogDestination);
            this.Controls.Add(this.lbLogFile);
            this.Controls.Add(this.tbLogFile);
            this.Controls.Add(this.lbLogEnable);
            this.Controls.Add(this.cbLogEnable);
            this.Controls.Add(this.rbLogCSV);
            this.Controls.Add(this.rbLogXLSX);
            this.Controls.Add(this.rbLogXML);
            this.Controls.Add(this.lbLogFormat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogFrm";
            this.ShowInTaskbar = false;
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogDestination;
        private System.Windows.Forms.Label lbLogFile;
        private System.Windows.Forms.TextBox tbLogFile;
        private System.Windows.Forms.Label lbLogEnable;
        private System.Windows.Forms.CheckBox cbLogEnable;
        private System.Windows.Forms.RadioButton rbLogCSV;
        private System.Windows.Forms.RadioButton rbLogXLSX;
        private System.Windows.Forms.RadioButton rbLogXML;
        private System.Windows.Forms.Label lbLogFormat;
        private System.Windows.Forms.SaveFileDialog sfdDestination;
        private System.Windows.Forms.Label lbIpAddress;
        private System.Windows.Forms.TextBox tbIpAddress;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Label lbPort;
        private System.Windows.Forms.RadioButton rbLogUDP;
    }
}