using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MGT.Cardia
{
    static class Program
    {
        private static string configurationFileName = "Cardia.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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
