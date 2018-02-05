using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace DesktopInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            update();
            this.Top = 0;


            Window w = new Window(); // Create helper window
            w.Top = -100; // Location of new window is outside of visible part of screen
            w.Left = -100;
            w.Width = 1; // size of window is enough small to avoid its appearance at the beginning
            w.Height = 1;

            w.WindowStyle = WindowStyle.ToolWindow; // Set window style as ToolWindow to avoid its icon in AltTab 
            w.ShowInTaskbar = false;
            w.Show(); // We need to show window before set is as owner to our main window
            this.Owner = w; // Okey, this will result to disappear icon for main window.
            w.Hide(); // Hide helper window just in case

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        

        
        public static string GetLocalIPAddress()
        {
            try
            {
                // Get the IP  
                IPHostEntry ipHostEntry = Dns.GetHostEntry(GetHostname());
                IPAddress ipAddress = ipHostEntry.AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork); // ipv4

                //string myIP = Dns.GetHostAddresses(Dns.GetHostName());
                string myIP = ipAddress.ToString();
                return myIP;
            }
            catch
            {
                return "No Connection";
            }
        }
        public static string GetBiosVersion() {
            ManagementClass managementClass = new ManagementClass("Win32_BIOS");
            ManagementObjectCollection instances = managementClass.GetInstances();
            string version = null;
            foreach (ManagementBaseObject instance in instances)
            {
                if(version == null)
                    version = instance.Properties["SMBIOSBIOSVersion"].Value.ToString();
            }
            return version.ToString();
        }

        public static string GetHostname() {
            return System.Environment.MachineName;
        }

        public static TimeSpan GetUpTime()
        {
            var ticks = Stopwatch.GetTimestamp();
            var uptime = ((double)ticks) / Stopwatch.Frequency;
            var uptimeSpan = TimeSpan.FromSeconds(uptime);
            return uptimeSpan;
        }

        public static string GetOSVersion()
        {
            var query = "SELECT * FROM Win32_OperatingSystem";
            var searcher = new ManagementObjectSearcher(query);
            var info = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
            var caption = info.Properties["Caption"].Value.ToString();
            var version = info.Properties["Version"].Value.ToString();
            var spMajorVersion = info.Properties["ServicePackMajorVersion"].Value.ToString();
            var spMinorVersion = info.Properties["ServicePackMinorVersion"].Value.ToString();

            return version;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            update();
        }

        void update() {
            hostnameLabel.Content = "Hostname: \t" + GetHostname();
            ipLabel.Content = "IP Address: \t" + GetLocalIPAddress();
            biosLabel.Content = "BIOS Ver: \t" + GetBiosVersion();
            osLabel.Content = "OS Version: \t" + GetOSVersion();
            if(GetUpTime().Days == 0)
            {
                uptimeLabel.Content = "Last Reboot: \t" + GetUpTime().ToString(@"hh\:mm\:ss");
            }
            else if(GetUpTime().Days == 1)
            {
                uptimeLabel.Content = "Last Reboot: \t" + GetUpTime().ToString("%d' day '") + GetUpTime().ToString(@"hh\:mm\:ss");
            }
            else
            {
                uptimeLabel.Content = "Last Reboot: \t" + GetUpTime().ToString("%d' days '") + GetUpTime().ToString(@"hh\:mm\:ss");
            }

            updateFramePos();
        }

        void updateFramePos() {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            string baseline = uptimeLabel.Content.ToString();
            //Measure the length of the string, baseline, in pixels.
            System.Drawing.Size size = TextRenderer.MeasureText(baseline, new Font("Segoe UI", 12));
            uptimeLabel.Width = size.Width;
            hostnameLabel.Width = size.Width;
            ipLabel.Width = size.Width;
            biosLabel.Width = size.Width;
            osLabel.Width = size.Width;

            frame.Width = size.Width + 10;

            frame.Left = desktopWorkingArea.Right - frame.Width;
        }
    }
}
