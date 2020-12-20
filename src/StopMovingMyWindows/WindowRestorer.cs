using System;
using System.Collections.Generic;
using System.Threading;

namespace StopMovingMyWindows
{
    /// <summary>
    /// Static class for handling window saving and restoration.
    /// </summary>
    public static class WindowRestorer
    {
        private static readonly object Lck = new object();
        private static bool isSuspended;
        private static IDictionary<IntPtr, WindowPlacement>? windowStates;

        /// <summary>
        /// Starts the capture of window positions.
        /// </summary>
        public static void StartCapture()
        {
            Capture();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(500);
                    Capture();
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// Saves the window states.
        /// </summary>
        public static void SaveWindowStates()
        {
            lock (Lck)
            {
                isSuspended = true;
            }
        }

        /// <summary>
        /// Restores the window states.
        /// </summary>
        public static void RestoreWindowStates()
        {
            lock (Lck)
            {
                if (windowStates != null)
                {
                    WindowManager.SetWindowStates(windowStates);
                    isSuspended = false;
                }
            }
        }

        private static void Capture()
        {
            lock (Lck)
            {
                if (!isSuspended)
                {
                    windowStates = WindowManager.GetWindowStates();
                }
            }
        }
    }
}