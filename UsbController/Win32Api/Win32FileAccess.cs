using System;

namespace UsbController.Win32Api
{
    /// <summary>
    /// Defines the available access mode when opening a file.
    /// </summary>
    /// <seealso cref="Kernel32Methods.CreateFile"/>
    /// <remarks>
    /// Details: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
    /// </remarks>
    [Flags]
    public enum Win32FileAccess : uint
    {
        /// <summary>
        /// Open the file for reading.
        /// </summary>
        GenericRead = 0x80000000,

        /// <summary>
        /// Open the file for writing.
        /// </summary>
        GenericWrite = 0x40000000,

        /// <summary>
        /// Open the file for executing.
        /// </summary>
        GenericExecute = 0x20000000,

        /// <summary>
        /// All accesses.
        /// </summary>
        GenericAll = 0x10000000,
    }
}
