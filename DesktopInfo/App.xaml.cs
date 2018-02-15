using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopInfo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private double fontsize = 12;
        private bool disablesystemtray = false;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                loadConfig();
            }
            catch {}
            MainWindow mainView = new MainWindow(disablesystemtray, fontsize);
            mainView.Show();
        }

        public void loadConfig() {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@".\config.dat");
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().StartsWith("disablesystemtray"))
                {
                    disablesystemtray = true;
                }
                else if (line.ToLower().StartsWith("fontsize")) {
                    fontsize =  Convert.ToDouble(line.Split(':')[1]);
                }
                counter++;
            }
            file.Close();
        }
    }
}
