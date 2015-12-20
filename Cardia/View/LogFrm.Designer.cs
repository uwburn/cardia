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
            this.lbLogDestination = new System.Windows.Forms.Label();
            this.tbLogDestination = new System.Windows.Forms.TextBox();
            this.lbLogEnable = new System.Windows.Forms.Label();
            this.cbLogEnable = new System.Windows.Forms.CheckBox();
            this.rbLogCSV = new System.Windows.Forms.RadioButton();
            this.rbLogXLSX = new System.Windows.Forms.RadioButton();
            this.rbLogXML = new System.Windows.Forms.RadioButton();
            this.lbLogFormat = new System.Windows.Forms.Label();
            this.sfdDestination = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnLogDestination
            // 
            this.btnLogDestination.Location = new System.Drawing.Point(319, 94);
            this.btnLogDestination.Name = "btnLogDestination";
            this.btnLogDestination.Size = new System.Drawing.Size(27, 23);
            this.btnLogDestination.TabIndex = 27;
            this.btnLogDestination.Text = "...";
            this.btnLogDestination.UseVisualStyleBackColor = true;
            this.btnLogDestination.Click += new System.EventHandler(this.btnLogDestination_Click);
            // 
            // lbLogDestination
            // 
            this.lbLogDestination.Location = new System.Drawing.Point(12, 94);
            this.lbLogDestination.Name = "lbLogDestination";
            this.lbLogDestination.Size = new System.Drawing.Size(81, 23);
            this.lbLogDestination.TabIndex = 26;
            this.lbLogDestination.Text = "Destination:";
            this.lbLogDestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbLogDestination
            // 
            this.tbLogDestination.Location = new System.Drawing.Point(99, 96);
            this.tbLogDestination.Name = "tbLogDestination";
            this.tbLogDestination.Size = new System.Drawing.Size(214, 20);
            this.tbLogDestination.TabIndex = 25;
            this.tbLogDestination.TextChanged += new System.EventHandler(this.tbLogDestination_TextChanged);
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
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 135);
            this.Controls.Add(this.btnLogDestination);
            this.Controls.Add(this.lbLogDestination);
            this.Controls.Add(this.tbLogDestination);
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
            this.Name = "Log";
            this.ShowInTaskbar = false;
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Log_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogDestination;
        private System.Windows.Forms.Label lbLogDestination;
        private System.Windows.Forms.TextBox tbLogDestination;
        private System.Windows.Forms.Label lbLogEnable;
        private System.Windows.Forms.CheckBox cbLogEnable;
        private System.Windows.Forms.RadioButton rbLogCSV;
        private System.Windows.Forms.RadioButton rbLogXLSX;
        private System.Windows.Forms.RadioButton rbLogXML;
        private System.Windows.Forms.Label lbLogFormat;
        private System.Windows.Forms.SaveFileDialog sfdDestination;
    }
}