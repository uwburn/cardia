using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.Utilities.Network;
using MGT.HRM;
using MGT.HRM.Emulator;
using MGT.HRM.Zephyr_HxM;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    public partial class LogFrm : Form
    {
        Cardia cardia;

        public LogFrm(Cardia cardia)
        {
            this.cardia = cardia;

            InitializeComponent();

            cardia_LoggerChanged(this, cardia.LogFormat);

            cardia.LoggerChanged += Cardia_LoggerChanged;
            cardia.LogEnabledChanged += cardia_LogEnabledChanged;
            cardia.LogFormatChanged += cardia_LoggerChanged;

            cbLogEnable_CheckedChanged(this, null);
            rbLogCSV_CheckedChanged(this, null);
            rbLogXLSX_CheckedChanged(this, null);
            rbLogXML_CheckedChanged(this, null);
            rbLogUDP_CheckedChanged(this, null);
        }

        private void Cardia_LoggerChanged(object sender, IHRMLogger arg)
        {
            if (arg is IHRMNetLogger)
            {
                tbIpAddress.Text = ((IHRMNetLogger)arg).Address;
                nudPort.Value = ((IHRMNetLogger)arg).Port;
            }
            else if (arg is IHRMFileLogger)
            {
                tbLogFile.Text = ((IHRMFileLogger)arg).FileName;
            }
        }

        void cardia_LogEnabledChanged(object sender, bool enabled)
        {
            this.cbLogEnable.Checked = enabled;
        }

        void cardia_LoggerChanged(object sender, LogFormat loggerType)
        {
            switch (loggerType)
            {
                case LogFormat.CSV:
                    rbLogCSV.Checked = true;
                    break;
                case LogFormat.XLSX:
                    rbLogXLSX.Checked = true;
                    break;
                case LogFormat.XML:
                    rbLogXML.Checked = true;
                    break;
                case LogFormat.UDP:
                    rbLogUDP.Checked = true;
                    break;
            }

            LogTypeUI();
        }

        private void Log_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void rbLogCSV_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbLogCSV.Checked)
                return;

            sfdDestination.DefaultExt = "csv";
            sfdDestination.Filter = "Comma Separated Values|*.csv";

            cardia.LogFormat = LogFormat.CSV;

            tbLogDestination_TextChanged(this, null);
        }

        private void rbLogXLSX_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbLogXLSX.Checked)
                return;

            sfdDestination.DefaultExt = "xlsx";
            sfdDestination.Filter = "Excel Spreadsheet|*.xlsx";

            cardia.LogFormat = LogFormat.XLSX;

            tbLogDestination_TextChanged(this, null);
        }

        private void rbLogXML_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbLogXML.Checked)
                return;

            sfdDestination.DefaultExt = "xml";
            sfdDestination.Filter = "Extensible Markup Language|*.xml";

            cardia.LogFormat = LogFormat.XML;

            tbLogDestination_TextChanged(this, null);
        }

        private void rbLogUDP_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbLogUDP.Checked)
                return;

            cardia.LogFormat = LogFormat.UDP;

            tbIpAddress_TextChanged(this, null);
            nudPort_ValueChanged(this, null);
        }

        private void cbLogEnable_CheckedChanged(object sender, EventArgs e)
        {
            cardia.LogEnabled = cbLogEnable.Checked;

            if (cbLogEnable.Checked)
            {
                lbLogFormat.Enabled = true;
                rbLogCSV.Enabled = true;
                rbLogXLSX.Enabled = true;
                rbLogXML.Enabled = true;
                rbLogUDP.Enabled = true;
            }
            else
            {
                lbLogFormat.Enabled = false;
                rbLogCSV.Enabled = false;
                rbLogXLSX.Enabled = false;
                rbLogXML.Enabled = false;
                rbLogUDP.Enabled = false;
            }

            LogTypeUI();
        }

        private void sfdDestination_FileOk(object sender, CancelEventArgs e)
        {
            tbLogFile.Text = sfdDestination.FileName;
        }

        private void btnLogDestination_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.IsReadOnly.ToString();

            DateTime now = DateTime.Now;

            sfdDestination.FileName = "Cardia_Log_" + now.ToString("yyyy-MM-dd_HH-mm");
            sfdDestination.ShowDialog();
        }

        private void tbLogDestination_TextChanged(object sender, EventArgs e)
        {
            if (cardia.Logger != null)
            {
                if (cardia.Logger is IHRMFileLogger)
                {
                    IHRMFileLogger fileLogger = (IHRMFileLogger)cardia.Logger;
                    fileLogger.FileName = tbLogFile.Text;
                }
            }
        }

        private void tbIpAddress_TextChanged(object sender, EventArgs e)
        {
            if (cardia.Logger != null)
            {
                if (cardia.Logger is IHRMNetLogger)
                {
                    IHRMNetLogger netLogger = (IHRMNetLogger)cardia.Logger;
                    netLogger.Address = tbIpAddress.Text;
                }
            }
        }

        private void nudPort_ValueChanged(object sender, EventArgs e)
        {
            if (cardia.Logger != null)
            {
                if (cardia.Logger is IHRMNetLogger)
                {
                    IHRMNetLogger netLogger = (IHRMNetLogger)cardia.Logger;
                    netLogger.Port = Convert.ToInt32(nudPort.Value);
                }
            }
        }

        public void LockUI()
        {
            cbLogEnable.Enabled = false;
            lbLogEnable.Enabled = false;
            lbLogFormat.Enabled = false;
            rbLogCSV.Enabled = false;
            rbLogXLSX.Enabled = false;
            rbLogXML.Enabled = false;
            rbLogUDP.Enabled = false;
            lbLogFile.Enabled = false;
            tbLogFile.Enabled = false;
            lbIpAddress.Enabled = false;
            tbIpAddress.Enabled = false;
            lbPort.Enabled = false;
            nudPort.Enabled = false;
            btnLogDestination.Enabled = false;
        }

        private void LogTypeUI()
        {
            bool fileLogger = false;
            bool netLogger = false;
            switch (cardia.LogFormat)
            {
                case LogFormat.CSV:
                case LogFormat.XLSX:
                case LogFormat.XML:
                    fileLogger = true;
                    break;
                case LogFormat.UDP:
                    netLogger = true;
                    break;
            }

            if (cardia.LogEnabled)
            {
                lbLogFile.Enabled = fileLogger;
                tbLogFile.Enabled = fileLogger;
                lbIpAddress.Enabled = netLogger;
                tbIpAddress.Enabled = netLogger;
                lbPort.Enabled = netLogger;
                nudPort.Enabled = netLogger;
                btnLogDestination.Enabled = fileLogger;
            }
            else
            {
                lbLogFile.Enabled = false;
                tbLogFile.Enabled = false;
                lbIpAddress.Enabled = false;
                tbIpAddress.Enabled = false;
                lbPort.Enabled = false;
                nudPort.Enabled = false;
                btnLogDestination.Enabled = false;
            }
        }

        public void ResetUI()
        {
            cbLogEnable.Enabled = true;
            lbLogEnable.Enabled = true;
            cbLogEnable_CheckedChanged(this, null);
        }
    }
}
