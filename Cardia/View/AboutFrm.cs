using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace MGT.Cardia
{
    partial class AboutFrm : Form
    {
        private static readonly AboutFrm instance = new AboutFrm();

        public static AboutFrm Instance
        {
            get
            {
                return instance;
            }
        }

        private AboutFrm()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyAttributes.AssemblyTitle);
            this.labelProductName.Text = AssemblyAttributes.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyAttributes.AssemblyFullVersion);
            this.labelCopyright.Text = AssemblyAttributes.AssemblyCopyright;
            this.textBoxDescription.Text = Resources.Description;
        }

        private void About_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://github.com/uwburn/cardia");
            Process.Start(sInfo);
        }
    }
}
