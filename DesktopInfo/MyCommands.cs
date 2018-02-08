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
            GetTaskbarWindow(parameter);

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            Window w = new Window(); // Create helper window
            w.Width = 200; // size of window is enough small to avoid its appearance at the beginning
            w.Height = 200;
            w.Top = desktopWorkingArea.Bottom / 2 - w.Height / 2;// Location of new window is outside of visible part of screen
            w.Left = desktopWorkingArea.Right / 2 - w.Width / 2;

            w.WindowStyle = WindowStyle.SingleBorderWindow; // Set window style as ToolWindow to avoid its icon in AltTab 

            var grid = new Grid { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Label CopyrightLabel = new Label { Content = "Desktop Info\n© 2018 ThirdISoftware" };
            Grid.SetColumn(CopyrightLabel, 0);
            Grid.SetRow(CopyrightLabel, 0);
            grid.Children.Add(CopyrightLabel);
            w.Content = grid;
            w.Show();

            CommandManager.InvalidateRequerySuggested();
        }

        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null;
        }
    }
}