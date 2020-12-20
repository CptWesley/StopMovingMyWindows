using System;
using System.Runtime.InteropServices;

/// <summary>
/// Manages native interop for device events.
/// </summary>
internal static class DeviceEvents
{
    /// <summary>
    /// Denotes that a device was added.
    /// </summary>
    public const int DbtDeviceArrival = 0x8000;

    /// <summary>
    /// Denotes that a device was removed.
    /// </summary>
    public const int DbtDeviceRemoveComplete = 0x8004;

    /// <summary>
    /// Denotes a device changed event.
    /// </summary>
    public const int WmDevicechange = 0x0219;

    private const int DbtDevtypDeviceinterface = 5;
    private static readonly Guid GuidDevInterfaceMonitor = new Guid("E6F07B5F-EE97-4a90-B076-33F57BF4EAA7");

    /// <summary>
    /// Registers a window to receive notifications when devices are plugged or unplugged.
    /// </summary>
    /// <param name="windowHandle">Handle to the window receiving notifications.</param>
    public static void RegisterDeviceNotification(IntPtr windowHandle)
    {
        DevBroadcastDeviceinterface dbi = new DevBroadcastDeviceinterface
        {
            DeviceType = DbtDevtypDeviceinterface,
            Reserved = 0,
            ClassGuid = GuidDevInterfaceMonitor,
            Name = 0,
        };

        dbi.Size = Marshal.SizeOf(dbi);
        IntPtr buffer = Marshal.AllocHGlobal(dbi.Size);
        Marshal.StructureToPtr(dbi, buffer, true);
        NativeMethods.RegisterDeviceNotification(windowHandle, buffer, 0);
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct DevBroadcastDeviceinterface
    {
        internal int Size;
        internal int DeviceType;
        internal int Reserved;
        internal Guid ClassGuid;
        internal short Name;
    }

    private static class NativeMethods
    {
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);
    }
}