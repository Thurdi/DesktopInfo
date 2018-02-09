using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopInfo.Commands
{
    /// <summary>
    /// Shows the main window.
    /// </summary>
    public class Exit : CommandBase<Exit>
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
    public class About : CommandBase<About>
    {
        public override void Execute(object parameter)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            aboutWindow win = new aboutWindow();
            win.Top = desktopWorkingArea.Bottom / 2 - win.Height / 2;// Location of new window is outside of visible part of screen
            win.Left = desktopWorkingArea.Right / 2 - win.Width / 2;
            win.Show();


            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}