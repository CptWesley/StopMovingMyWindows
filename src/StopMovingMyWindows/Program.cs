using System;
using System.Windows.Forms;

namespace StopMovingMyWindows
{
    /// <summary>
    /// The entry class of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            WindowManager.GetWindowStates();
            Application.EnableVisualStyles();
            using EventWindow window = new EventWindow();
            WindowRestorer.StartCapture();
            Application.Run(window);
        }
    }
}
