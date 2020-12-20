using System.Drawing;
using System.Runtime.InteropServices;

namespace StopMovingMyWindows
{
    /// <summary>
    /// Structure used for invoking Windows API.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPlacement
    {
        /// <summary>
        /// The length of the struct.
        /// </summary>
        public int Length;

        /// <summary>
        /// The flags of the window.
        /// </summary>
        public uint Flags;

        /// <summary>
        /// Whether or not to show the cmd.
        /// </summary>
        public uint ShowCmd;

        /// <summary>
        /// The minimum position.
        /// </summary>
        public Point PtMinPosition;

        /// <summary>
        /// The maximum position.
        /// </summary>
        public Point PtMaxPosition;

        /// <summary>
        /// The window rectangle.
        /// </summary>
        public Rectangle RcNormalPosition;

        /// <summary>
        /// Creates a copy of this instance.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public WindowPlacement Copy()
            => new WindowPlacement
            {
                Length = Length,
                Flags = Flags,
                ShowCmd = ShowCmd,
                PtMinPosition = PtMinPosition,
                PtMaxPosition = PtMaxPosition,
                RcNormalPosition = RcNormalPosition,
            };
    }
}
