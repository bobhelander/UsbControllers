using System;
using System.Runtime.InteropServices;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Provides details about a single USB device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class DeviceInterfaceData
    {
        /// <summary>
        /// The size of the object.
        /// </summary>
        private int size;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInterfaceData"/> class.
        /// </summary>
        public DeviceInterfaceData()
        {
            this.size = Marshal.SizeOf(typeof(DeviceInterfaceData));
        }

        /// <summary>
        /// Gets the size of the object.
        /// </summary>
        /// <value>
        /// Will be equal to <c>Marshal.SiezOf(typeof(DeviceInterfaceData)</c>.
        /// </value>
        public int Size { get { return this.size; } }

        /// <summary>
        /// Gets or sets the Guid of the interface.
        /// </summary>
        public Guid InterfaceClassGuid { get; set; }

        /// <summary>
        /// Gets or sets the flags for this device.
        /// </summary>
        /// TODO: Use an enumeration
        public int Flags { get; set; }

        /// <summary>
        /// A reserved space - should not be used.
        /// </summary>
        public int Reserved { get; set; }
    }
}
