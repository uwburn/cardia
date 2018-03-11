using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MGT.HRM.CMS50;
using MGT.HRM;

namespace MGT.Cardia
{
    public partial class CMS50Frm : HRMDeviceFrm
    {
        private CMS50 cms50;
        List<string> serialPortNames;

        public CMS50Frm(CMS50Bundle bundle)
        {
            InitializeComponent();

            this.cms50 = bundle.CMS50;
            serialPortNames = bundle.SerialPorts;

            bundle.Started += bundle_Started;
            bundle.Stopped += bundle_Stopped;
            cms50.PacketProcessed += cms50_OnPacketProcessed;
            cms50.SerialPortChanged += cms50_SerialPortChanged;

            foreach (string serialPortName in serialPortNames)
            {
                cbSerialPorts.Items.Add(serialPortName);
            }

            if (serialPortNames.Count > 0)
            {
                if (cms50.SerialPort != null)
                {
                    foreach (string serialPortName in cbSerialPorts.Items)
                    {
                        if (serialPortName == cms50.SerialPort)
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

        public void cms50_OnPacketProcessed(object sender, PacketProcessedEventArgs status)
        {
            if (this.IsHandleCreated)
            {
                CMS50Packet lastPacket = (CMS50Packet)status.HRMPacket;

                this.BeginInvoke(new MethodInvoker(delegate() {
                    this.SuspendLayout();

                    tbHeartRate.Text = lastPacket.HeartRate.ToString();
                    tbHeartbeatCount.Text = cms50.HeartBeats.ToString();
                    tbBeat.Text = lastPacket.Beep.ToString();
                    tbSpO2.Text = lastPacket.SpO2.ToString();
                    tbSpO2Drop.Text = lastPacket.SpO2Drop.ToString();
                    tbWaveform.Text = lastPacket.Waveform.ToString();
                    tbBarGraph.Text = lastPacket.BarGraph.ToString();
                    if (lastPacket.SignalStrength > pbStrength.Maximum)
                        pbStrength.Maximum = lastPacket.SignalStrength;
                    pbStrength.Value = lastPacket.SignalStrength;

                    string strStatus = "OK";
                    bool flgStatus = false;
                    if (lastPacket.ProbeError)
                    {
                        strStatus = "Probe error";
                        flgStatus = true;
                    }
                    if (lastPacket.Searching)
                    {
                        if (flgStatus)
                            strStatus += " - Searching";
                        else
                            strStatus = "Searching";
                        flgStatus = true;
                    }
                    if (lastPacket.SearchingTooLong)
                    {
                        if (flgStatus)
                            strStatus += " - Searching too long";
                        else
                            strStatus = "Searching too long";
                        flgStatus = true;
                    }

                    tbStatus.Text = strStatus;
                    this.ResumeLayout();
                }));
            }
        }

        void cms50_SerialPortChanged(object sender, string serialPort)
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

            cms50.SerialPort = serialPort;
        }

        private void CMS50Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
