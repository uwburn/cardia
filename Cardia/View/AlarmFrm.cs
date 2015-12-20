using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    public partial class AlarmFrm : Form
    {
        private Cardia cardia;

        public AlarmFrm(Cardia cardia)
        {
            this.cardia = cardia;

            InitializeComponent();

            cardia.AlarmEnabledChanged += cardia_AlarmEnabledChanged;
            cardia.AlarmLowThresholdChanged += cardia_AlarmLowThresholdChanged;
            cardia.AlarmHighThresholdChanged += cardia_AlarmHighThresholdChanged;
            cardia.AlarmDefuseChanged += cardia_AlarmDefuseChanged;
            cardia.AlarmDefuseTimeChanged += cardia_AlarmDefuseTimeChanged;
        }

        void cardia_AlarmEnabledChanged(object sender, bool arg)
        {
            cbAlarmEnable.Checked = arg;

            lbAlarmLowThreshold.Enabled = arg;
            nudAlarmLowThreshold.Enabled = arg;
            lbAlarmHighThreshold.Enabled = arg;
            nudAlarmHighThreshold.Enabled = arg;
            cbAlarmDefuse.Enabled = arg;
            lbAlarmDefuse.Enabled = arg;
            if (cardia.AlarmDefuse || !arg)
            {
                nudAlarmDefuseTime.Enabled = arg;
                lbAlarmDefuseTime.Enabled = arg;
            }
        }

        void cardia_AlarmLowThresholdChanged(object sender, int arg)
        {
            nudAlarmLowThreshold.Value = arg;
        }

        void cardia_AlarmHighThresholdChanged(object sender, int arg)
        {
            nudAlarmHighThreshold.Value = arg;
        }

        void cardia_AlarmDefuseChanged(object sender, bool arg)
        {
            cbAlarmDefuse.Checked = arg;

            nudAlarmDefuseTime.Enabled = arg;
            lbAlarmDefuseTime.Enabled = arg;
        }

        void cardia_AlarmDefuseTimeChanged(object sender, int arg)
        {
            nudAlarmDefuseTime.Value = arg;
        }

        private void cbAlarmEnable_CheckedChanged(object sender, EventArgs e)
        {
            cardia.AlarmEnabled = cbAlarmEnable.Checked;
        }

        private void nudAlarmLowThreshold_ValueChanged(object sender, EventArgs e)
        {
            cardia.AlarmLowThreshold = Convert.ToInt32(nudAlarmLowThreshold.Value);
        }

        private void nudAlarmHighThreshold_ValueChanged(object sender, EventArgs e)
        {
            cardia.AlarmHighThreshold = Convert.ToInt32(nudAlarmHighThreshold.Value);
        }

        private void cbAlarmDefuse_CheckedChanged(object sender, EventArgs e)
        {
            cardia.AlarmDefuse = cbAlarmDefuse.Checked;
        }

        private void nudAlarmDefuseTime_ValueChanged(object sender, EventArgs e)
        {
            cardia.AlarmDefuseTime = Convert.ToInt32(nudAlarmDefuseTime.Value);
        }

        private void AlarmFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }
    }
}
