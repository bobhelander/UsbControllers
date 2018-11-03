using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Provides direct access to the methods or the "kernel32.dll" library.
    /// </summary>
    public static class Kernel32Methods
    {
        /// <summary>
        /// Creates or opens a file or I/O device.
        /// </summary>
        /// <param name="name">The name of the file or device to be created or opened.</param>
        /// <param name="desiredAccess">The requested access to the file or device.</param>
        /// <param name="shareMode">The requested sharing mode of the file or device</param>
        /// <param name="security"></param>
        /// <param name="creationFlags"></param>
        /// <param name="attributes"></param>
        /// <param name="template"></param>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The returned element implements <see cref="IDisposable"/> and closes the handle
        /// properly when disposing.
        /// </para>
        /// <para>
        /// Developer Note: CharSet is set to Ansi - or None - because this is a requirement for this method
        /// when connecting to USB devices.
        /// Solution found here: http://social.msdn.microsoft.com/forums/en-us/vcgeneral/thread/1A77C595-10A5-4F14-8512-C67535C53A08
        /// </para>
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx"/>
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern SafeFileHandle CreateFile(
            [MarshalAs(UnmanagedType.LPStr)] string name,
            Win32FileAccess desiredAccess,
            FileShare shareMode,
            IntPtr security,
            FileMode creationFlags,
            Win32FileAttributes attributes,
            IntPtr template);
    }
}
