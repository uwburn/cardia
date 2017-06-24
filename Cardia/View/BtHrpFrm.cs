using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.HRM;
using MGT.HRM.HRP;
using System.IO.Ports;
using MGT.Utilities.EventHandlers;
using Windows.Devices.Enumeration;
using System.Diagnostics;

namespace MGT.Cardia
{
    public partial class BtHrpFrm : HRMDeviceFrm
    {
        private Cardia cardia;
        private BtHrp btHrp;
        DeviceInformationCollection devices;

        public BtHrpFrm(Cardia cardia)
        {
            InitializeComponent();

            this.cardia = cardia;
            this.btHrp = (BtHrp)cardia.HRM;
            devices = cardia.BtSmartDevices;

            cardia.Started += cardia_Started;
            cardia.Stopped += cardia_Stopped;
            cardia.PacketProcessed += cardia_OnPacketProcessed;
            btHrp.DeviceChanged += btHrp_DeviceChanged;

            cbDevices.DataSource = new BindingSource(devices, null);

            if (devices.Count > 0)
            {
                if (btHrp.Device != null)
                {
                    foreach (DeviceInformation device in cbDevices.Items)
                    {
                        if (device.Id == btHrp.Device.Id)
                        {
                            cbDevices.SelectedItem = device;
                            break;
                        }
                    }
                }
                else
                {
                    cbDevices.SelectedItem = devices[0];
                }
            }
        }

        void cardia_Started(object sender)
        {
            LockConfigurationUI();
        }

        void cardia_Stopped(object sender)
        {
            UnlockConfigurationUI();
        }

        public override void LockConfigurationUI()
        {
            cbDevices.Enabled = false;
        }

        public override void UnlockConfigurationUI()
        {
            cbDevices.Enabled = true;
        }

        public override void ResetUI()
        {
            cbDevices.Enabled = true;
        }

        public void cardia_OnPacketProcessed(object sender, HRMStatus status)
        {
            if (this.IsHandleCreated)
            {
                BtHrpPacket lastPacket = (BtHrpPacket)status.HRMPacket;

                this.BeginInvoke(new MethodInvoker(delegate() {
                    this.SuspendLayout();

                    tbHeartRate.Text = lastPacket.HeartRate.ToString();

                    this.ResumeLayout();
                }));
            }
        }

        void btHrp_DeviceChanged(object sender, DeviceInformation device)
        {
            foreach (DeviceInformation cbDevice in cbDevices.Items)
            {
                if (cbDevice.Id == device.Id)
                {
                    cbDevices.SelectedItem = device;
                    break;
                }
            }
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceInformation device = (DeviceInformation)cbDevices.SelectedItem;

            btHrp.Device = device;
        }

        private void BtHrpFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
