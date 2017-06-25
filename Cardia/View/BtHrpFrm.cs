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
            btHrp.CharacteristicIndexChanged += btHrp_CharacteristicIndexChanged;
            btHrp.InitDelayChanged += btHrp_InitDelayChanged;

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

            nudCharacteristic.Value = btHrp.CharacteristicIndex;

            nudInitDelay.Value = btHrp.InitDelay;
        }

        void cardia_Started(object sender)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    LockConfigurationUI();
                });
            }
            else
            {
                LockConfigurationUI();
            }
        }

        void cardia_Stopped(object sender)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    UnlockConfigurationUI();
                    tbHeartRate.Text = string.Empty;
                });
            }
            else
            {
                UnlockConfigurationUI();
                tbHeartRate.Text = string.Empty;
            }
        }

        public override void LockConfigurationUI()
        {
            cbDevices.Enabled = false;
            nudCharacteristic.Enabled = false;
            nudInitDelay.Enabled = false;
        }

        public override void UnlockConfigurationUI()
        {
            cbDevices.Enabled = true;
            nudCharacteristic.Enabled = true;
            nudInitDelay.Enabled = true;
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

        void btHrp_CharacteristicIndexChanged(object sender, int characteristicIndex)
        {
            nudCharacteristic.Value = characteristicIndex;
        }

        void btHrp_InitDelayChanged(object sender, int delay)
        {
            nudInitDelay.Value = delay;
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceInformation device = (DeviceInformation)cbDevices.SelectedItem;

            btHrp.Device = device;
        }

        private void nudCharacteristic_ValueChanged(object sender, EventArgs e)
        {
            btHrp.CharacteristicIndex = Decimal.ToInt32(nudCharacteristic.Value);
        }

        private void nudInitDelay_ValueChanged(object sender, EventArgs e)
        {
            btHrp.InitDelay = Decimal.ToInt32(nudInitDelay.Value);
        }

        private void BtHrpFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
