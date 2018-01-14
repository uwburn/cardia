using log4net;
using System;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "log4net.config"),]

namespace MGT.Cardia
{
    static class Program
    {
        public enum RpcAuthnLevel
        {
            Default = 0,
            None = 1,
            Connect = 2,
            Call = 3,
            Pkt = 4,
            PktIntegrity = 5,
            PktPrivacy = 6
        }

        public enum RpcImpLevel
        {
            Default = 0,
            Anonymous = 1,
            Identify = 2,
            Impersonate = 3,
            Delegate = 4
        }

        public enum EoAuthnCap
        {
            None = 0x00,
            MutualAuth = 0x01,
            StaticCloaking = 0x20,
            DynamicCloaking = 0x40,
            AnyAuthority = 0x80,
            MakeFullSIC = 0x100,
            Default = 0x800,
            SecureRefs = 0x02,
            AccessControl = 0x04,
            AppID = 0x08,
            Dynamic = 0x10,
            RequireFullSIC = 0x200,
            AutoImpersonate = 0x400,
            NoCustomMarshal = 0x2000,
            DisableAAA = 0x1000
        }

        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        public static extern int CoInitializeSecurity(IntPtr pVoid, int cAuthSvc, IntPtr asAuthSvc, IntPtr pReserved1, 
            RpcAuthnLevel level, RpcImpLevel impers, IntPtr pAuthList, EoAuthnCap dwCapabilities, IntPtr pReserved3);

        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string configurationFileName = "Cardia.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            logger.Info("Starting Cardia");
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Workaround for some Windows 10 versions
            CoInitializeSecurity(
                IntPtr.Zero,
                -1,
                IntPtr.Zero,
                IntPtr.Zero,
                RpcAuthnLevel.Default,
                RpcImpLevel.Identify,
                IntPtr.Zero,
                EoAuthnCap.None,
                IntPtr.Zero);

            Configuration configuration;
            try
            {
                configuration = Configuration.LoadFromFile(configurationFileName);
            }
            catch 
            {
                configuration = new Configuration();
            }

            Cardia cardia = new Cardia(configuration);

            /*if (DateTime.Today > cardia.ExpiryDate)
            {
                MessageBox.Show("This development preview release has expired.\nPlease download a new release from http://www.altairgarden.it/", "Release expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }*/

            CardiaFrm cardiaFrm = new CardiaFrm(cardia);
            ECGSound ecgSound = new ECGSound(cardia);
            cardia.Init();

            Application.Run(cardiaFrm);

            ecgSound.Dispose();

            try
            {
                cardia.SaveConfig();
                configuration.SaveToFile(configurationFileName);
            }
            catch { }
        }
    }
}
