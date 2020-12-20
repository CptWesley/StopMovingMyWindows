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
            Application.EnableVisualStyles();
            using EventWindow windowRestorer = new EventWindow();
            Application.Run(windowRestorer);
        }
    }
}
