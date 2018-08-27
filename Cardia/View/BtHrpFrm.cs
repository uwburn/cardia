using System;
using System.Windows.Forms;
using MGT.HRM.HRP;
using Windows.Devices.Enumeration;
using MGT.HRM;

namespace MGT.Cardia
{
    public partial class BtHrpFrm : HRMDeviceFrm
    {
        private BtHrp btHrp;
        DeviceInformationCollection devices;

        public BtHrpFrm(BtHrpBundle bundle)
        {
            InitializeComponent();

            btHrp = bundle.BtHrp;
            devices = bundle.BtSmartDevices;

            bundle.Started += bundle_Started;
            bundle.Stopped += bundle_Stopped;
            btHrp.PacketProcessed += btHrp_OnPacketProcessed;
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

        void bundle_Started(object sender)
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

        void bundle_Stopped(object sender)
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
            tbHeartRate.Text = string.Empty;
        }

        public void btHrp_OnPacketProcessed(object sender, PacketProcessedEventArgs status)
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
