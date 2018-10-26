using System.Runtime.InteropServices;

namespace UsbController.Win32Api
{
    // TODO: Find why struct is required and how to set size to the right value
    /// <summary>
    /// Provides the path of a device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeviceInterfaceDetailData
    {
        /// <summary>
        /// The size of the instance.
        /// </summary>
        public int Size;

        /// <summary>
        /// The path of the device.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DevicePath;
    }
}
