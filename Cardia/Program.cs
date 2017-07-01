using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "log4net.config"), ]

namespace MGT.Cardia
{
    static class Program
    {
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
