using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.HRM;
using MGT.HRM.Zephyr_HxM;
using MGT.HRM.Emulator;

namespace MGT.Cardia
{
    public partial class HRMEmulatorFrm : HRMDeviceFrm
    {
        private HRMEmulator hrmEmulator;

        public HRMEmulatorFrm(HRMEmulatorBundle bundle)
        {
            InitializeComponent();

            this.hrmEmulator = bundle.HRMEmulator;
            nudMinBPM.Value = hrmEmulator.EmulatorMinBPM;
            nudMaxBPM.Value = hrmEmulator.EmulatorMaxBPM;

            bundle.Started += bundle_Started;
            bundle.Stopped += bundle_Stopped;
            hrmEmulator.EmulatorMinBPMChanged += hrmEmulator_EmulatorMinBPMChanged;
            hrmEmulator.EmulatorMaxBPMChanged += hrmEmulator_EmulatorMaxBPMChanged;
        }

        void bundle_Started(object sender)
        {
            LockConfigurationUI();
        }

        void bundle_Stopped(object sender)
        {
            UnlockConfigurationUI();
        }

        public override void LockConfigurationUI()
        {
            return;
        }

        public override void UnlockConfigurationUI()
        {
            return;
        }

        public override void ResetUI()
        {
            return;
        }

        private void HRM_Emulator_Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void nudMinBPM_ValueChanged(object sender, EventArgs e)
        {
            hrmEmulator.EmulatorMinBPM = Convert.ToInt32(nudMinBPM.Value);
        }

        private void nudMaxBPM_ValueChanged(object sender, EventArgs e)
        {
            hrmEmulator.EmulatorMaxBPM = Convert.ToInt32(nudMaxBPM.Value);
        }

        void hrmEmulator_EmulatorMinBPMChanged(object sender, int minBPM)
        {
            nudMinBPM.Value = minBPM;
        }

        void hrmEmulator_EmulatorMaxBPMChanged(object sender, int maxBPM)
        {
            nudMaxBPM.Value = maxBPM;
        }
    }
}
