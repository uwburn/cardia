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
using System.IO.Ports;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    public partial class ZephyrHxMFrm : HRMDeviceFrm
    {
        private ZephyrHxM zephyrHxM;
        List<string> serialPortNames;

        public ZephyrHxMFrm(ZephyrHxMBundle bundle)
        {
            InitializeComponent();

            this.zephyrHxM = bundle.ZephyrHxM;
            serialPortNames = bundle.SerialPorts;

            bundle.Started += bundle_Started;
            bundle.Stopped += bundle_Stopped;
            zephyrHxM.PacketProcessed += bundle_OnPacketProcessed;
            zephyrHxM.SerialPortChanged += zephyrHxM_SerialPortChanged;

            foreach (string serialPortName in serialPortNames)
            {
                cbSerialPorts.Items.Add(serialPortName);
            }

            if (serialPortNames.Count > 0)
            {
                if (zephyrHxM.SerialPort != null)
                {
                    foreach (string serialPortName in cbSerialPorts.Items)
                    {
                        if (serialPortName == zephyrHxM.SerialPort)
                        {
                            cbSerialPorts.SelectedItem = serialPortName;
                            break;
                        }
                    }
                }
                else
                {
                    cbSerialPorts.SelectedItem = serialPortNames[0];
                }
            }
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
            cbSerialPorts.Enabled = false;
        }

        public override void UnlockConfigurationUI()
        {
            cbSerialPorts.Enabled = true;
        }

        public override void ResetUI()
        {
            cbSerialPorts.Enabled = true;
        }

        public void bundle_OnPacketProcessed(object sender, PacketProcessedEventArgs status)
        {
            if (this.IsHandleCreated)
            {
                ZephyrPacket lastPacket = (ZephyrPacket)status.HRMPacket;

                this.BeginInvoke(new MethodInvoker(delegate() {
                    this.SuspendLayout();
                    pbBattery.Value = Convert.ToInt32(zephyrHxM.SmoothedBatteryLevel);
                    if (zephyrHxM.TotalPackets != 0)
                        pbIntegrity.Value = 100 - Convert.ToInt32(zephyrHxM.CorruptedPackets / zephyrHxM.TotalPackets) * 100;
                    else
                        pbIntegrity.Value = 0;

                    tbDeviceId.Text = lastPacket.hardwareId.ToString() + " - " + lastPacket.firmwareId.ToString();
                    tbHeartRate.Text = lastPacket.heartRate.ToString();
                    tbHeartbeatCount.Text = lastPacket.heartBeatNumber.ToString();
                    tbRR1.Text = lastPacket.rrIntervals[0].ToString();
                    tbRR2.Text = lastPacket.rrIntervals[1].ToString();
                    tbRR3.Text = lastPacket.rrIntervals[2].ToString();
                    tbRR4.Text = lastPacket.rrIntervals[3].ToString();
                    tbRR5.Text = lastPacket.rrIntervals[4].ToString();
                    tbRR6.Text = lastPacket.rrIntervals[5].ToString();
                    tbRR7.Text = lastPacket.rrIntervals[6].ToString();
                    tbRR8.Text = lastPacket.rrIntervals[7].ToString();
                    tbRR9.Text = lastPacket.rrIntervals[8].ToString();
                    tbRR10.Text = lastPacket.rrIntervals[9].ToString();
                    tbRR11.Text = lastPacket.rrIntervals[10].ToString();
                    tbRR12.Text = lastPacket.rrIntervals[11].ToString();
                    tbRR13.Text = lastPacket.rrIntervals[12].ToString();
                    tbRR14.Text = lastPacket.rrIntervals[13].ToString();
                    this.ResumeLayout();
                }));
            }
        }

        void zephyrHxM_SerialPortChanged(object sender, string serialPort)
        {
            foreach (string serialPortName in cbSerialPorts.Items)
            {
                if (serialPortName == serialPort)
                {
                    cbSerialPorts.SelectedItem = serialPortName;
                    break;
                }
            }
        }

        private void cbSerialPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string serialPort = (string)cbSerialPorts.SelectedItem;

            zephyrHxM.SerialPort = serialPort;
        }

        private void Zephyr_HxM_Panel_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
