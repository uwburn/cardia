using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGT.Utilities.Network;
using MGT.Utilities.EventHandlers;

namespace MGT.Cardia
{
    public partial class NetworkFrm : Form
    {
        private Cardia cardia;
        private NetworkRelay<HeartRateMessage> networkRelay;
        bool connected = false;

        public NetworkFrm(Cardia cardia)
        {
            this.cardia = cardia;

            InitializeComponent();

            cardia.NetworkModeChanged += cardia_NetworkModeChanged;
            cardia.NetworkStatusChenged += cardia_NetworkStatusChenged;
            cardia.NetworkRelayChanged += cardia_NetworkRelayChanged;
        }

        void cardia_NetworkRelayChanged(object sender, NetworkRelay<HeartRateMessage> networkRelay)
        {
            this.networkRelay = networkRelay;

            networkRelay.NicknameChanged += networkRelay_NicknameChanged;
            networkRelay.PortChanged += networkRelay_PortChanged;
            cardia.NetworkConnected += cardia_NetworkConnected;
            cardia.NetworkDisconnected += cardia_NetworkDisconnected;

            if (networkRelay is TcpRelayClient<HeartRateMessage>)
                ((TcpRelayClient<HeartRateMessage>)networkRelay).ServerAddressChanged += NetworkFrm_ServerAddressChanged;
        }

        void cardia_NetworkDisconnected(object sender, bool error)
        {
            this.Invoke((MethodInvoker)(
                delegate()
                {
                    if (error)
                        System.Media.SystemSounds.Hand.Play();

                    connected = false;
                    ResetUI();
                }
            ));
        }

        void cardia_NetworkConnected(object sender, string nickname)
        {
            this.Invoke((MethodInvoker)(
                delegate()
                {
                    connected = true;
                    LockUI();
                    btnConnect.Text = "Disconnect";
                }
            ));
        }

        void NetworkFrm_ServerAddressChanged(object sender, string serverAddress)
        {
            this.tbAddress.Text = serverAddress;
        }

        void networkRelay_PortChanged(object sender, int port)
        {
            this.tbPort.Text = port.ToString();
        }

        void networkRelay_NicknameChanged(object sender, string nickname)
        {
            this.tbNickname.Text = nickname;
        }

        void cardia_NetworkStatusChenged(object sender, string status)
        {
            tslStatus.Text = status;
        }

        void cardia_NetworkModeChanged(object sender, NetworkRelayMode mode)
        {
            switch(mode)
            {
                case NetworkRelayMode.Client:
                    this.SuspendLayout();
                    rbClient.Checked = true;
                    lbAddress.Visible = true;
                    tbAddress.Visible = true;
                    this.ResumeLayout();
                    break;
                case NetworkRelayMode.Server:
                    this.SuspendLayout();
                    rbServer.Checked = true;
                    lbAddress.Visible = false;
                    tbAddress.Visible = false;
                    this.ResumeLayout();
                    break;
            }
        }

        private void Network_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void rbServer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbServer.Checked)
                cardia.NetworkMode = NetworkRelayMode.Server;
        }

        private void rbClient_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClient.Checked)
                cardia.NetworkMode = NetworkRelayMode.Client;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                cardia.StartNetwork();
            }
            else
            {
                cardia.StopNetwork();
            }
        }

        public void LockUI()
        {
            tbNickname.Enabled = false;
            rbServer.Enabled = false;
            rbClient.Enabled = false;
            tbPort.Enabled = false;
            tbAddress.Enabled = false;
        }

        public void ResetUI()
        {
            connected = false;
            btnConnect.Text = "Connect";
            tbNickname.Enabled = true;
            rbServer.Enabled = true;
            rbClient.Enabled = true;
            tbPort.Enabled = true;
            tbAddress.Enabled = true;
        }

        private void tbNickname_TextChanged(object sender, EventArgs e)
        {
            networkRelay.Nickname = tbNickname.Text;
        }

        private void tbPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                networkRelay.Port = int.Parse(tbPort.Text);
            }
            catch {
                System.Media.SystemSounds.Hand.Play();
                tslStatus.Text = "Invalid port";
            }
        }

        private void tbAddress_TextChanged(object sender, EventArgs e)
        {
            if (networkRelay is TcpRelayClient<HeartRateMessage>)
                ((TcpRelayClient<HeartRateMessage>)networkRelay).ServerAddress = tbAddress.Text;
        }
    }
}
