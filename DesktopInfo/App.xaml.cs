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
        private double fontsize;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            loadConfig();
            MainWindow mainView = new MainWindow(fontsize);
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
                if (line.ToLower().StartsWith("fontsize")) {
                    //config.Add(line.Split(':')[0], line.Split(':')[line.Split(':').Length - 1]);
                    fontsize =  Convert.ToDouble(line.Split(':')[1]);
                }
                counter++;
            }
            file.Close();
        }
    }
}
