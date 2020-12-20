using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StopMovingMyWindows
{
    /// <summary>
    /// Used to manage windows.
    /// </summary>
    /// <summary>Contains functionality to get all the open windows.</summary>
    internal static class WindowManager
    {
        /// <summary>
        /// Gets the window states.
        /// </summary>
        /// <returns>The states of all open windows.</returns>
        public static IDictionary<IntPtr, WindowPlacement> GetWindowStates()
        {
            IntPtr shellWindow = NativeMethods.GetShellWindow();
            Dictionary<IntPtr, WindowPlacement> windows = new Dictionary<IntPtr, WindowPlacement>();

            NativeMethods.EnumWindows(
                (hWnd, lParam) =>
                {
                    if (hWnd == shellWindow || !NativeMethods.IsWindowVisible(hWnd))
                    {
                        return true;
                    }

                    WindowPlacement placement = default;
                    placement.Length = Marshal.SizeOf(placement);
                    NativeMethods.GetWindowPlacement(hWnd, ref placement);
                    windows[hWnd] = placement;
                    return true;
                }, 0);

            return windows;
        }

        /// <summary>
        /// Sets the window states.
        /// </summary>
        /// <param name="states">The states of all open windows.</param>
        public static void SetWindowStates(IDictionary<IntPtr, WindowPlacement> states)
        {
            foreach (KeyValuePair<IntPtr, WindowPlacement> entry in states)
            {
                NativeMethods.ShowWindow(entry.Key, 1);
                NativeMethods.SetWindowPlacement(entry.Key, entry.Value);
            }
        }

        private static class NativeMethods
        {
            public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool IsWindowVisible(IntPtr hWnd);

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetShellWindow();

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetWindowPlacement(IntPtr hWnd, WindowPlacement lpwndpl);

            [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        }
    }
}
