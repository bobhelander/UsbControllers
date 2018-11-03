using System;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Defines the constraints to retrieve device classes.
    /// </summary>
    [Flags]
    public enum DiGetClassFlags : uint
    {
        /// <summary>
        /// No specification provided.
        /// </summary>
        None = 0,

        /// <summary>
        /// </summary>
        /// <remarks>
        /// only valid with DeviceInterface.
        /// </remarks>
        Default = 0x01,

        /// <summary>
        /// The classes should be associated to active devices.
        /// </summary>
        Present = 0x02,

        /// <summary>
        /// 
        /// </summary>
        AllClasses = 0x04,

        /// <summary>
        /// 
        /// </summary>
        Profile = 0x08,

        /// <summary>
        /// The classes should represents device interfaces.
        /// </summary>
        DeviceInterface = 0x10,
    }
}
