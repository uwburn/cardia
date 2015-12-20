using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MGT.Cardia
{
    public class HRMDeviceFrm : Form
    {
        public virtual void LockConfigurationUI() { }
        public virtual void UnlockConfigurationUI() { }
        public virtual void ResetUI() { }
    }
}
