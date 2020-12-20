using System;

namespace StopMovingMyWindows
{
    /// <summary>
    /// Static class for handling window saving and restoration.
    /// </summary>
    public static class WindowRestorer
    {
        private static object lck = new object();

        /// <summary>
        /// Saves the window states.
        /// </summary>
        public static void SaveWindowStates()
        {
            lock (lck)
            {
                Console.WriteLine("Saving positions...");
            }
        }

        /// <summary>
        /// Restores the window states.
        /// </summary>
        public static void RestoreWindowStates()
        {
            lock (lck)
            {
                Console.WriteLine("Restoring positions...");
            }
        }
    }
}