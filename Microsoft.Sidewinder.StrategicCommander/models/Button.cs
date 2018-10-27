using System;

namespace Microsoft.Sidewinder.StrategicCommander.models
{
    /// <summary>
    /// Specifies the standard buttons of the Strategic Commander.
    /// </summary>
    public enum Button
    {
        /// <summary>
        /// The 1 button.
        /// </summary>
        Button1 = 0x0001,

        /// <summary>
        /// The 2 button.
        /// </summary>
        Button2 = 0x0002,

        /// <summary>
        /// The 3 button.
        /// </summary>
        Button3 = 0x0004,

        /// <summary>
        /// The 4 button.
        /// </summary>
        Button4 = 0x0008,

        /// <summary>
        /// The 5 button.
        /// </summary>
        Button5 = 0x0010,

        /// <summary>
        /// The 6 button.
        /// </summary>
        Button6 = 0x0020,

        /// <summary>
        /// The Zoom In button (+).
        /// </summary>
        ZoomIn = 0x0040,

        /// <summary>
        /// The Zoom Out button (-).
        /// </summary>
        ZoomOut = 0x0080,

        /// <summary>
        /// The top Shift button.
        /// </summary>
        Shift1 = 0x0100,

        /// <summary>
        /// The middle Shift button.
        /// </summary>
        Shift2 = 0x0200,

        /// <summary>
        /// The bottom Shift button.
        /// </summary>
        Shift3 = 0x0400,

        /// <summary>
        /// The Record button (R).
        /// </summary>
        Record = 0x0800,
    }
}
