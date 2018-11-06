using System;

namespace Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Models
{
    /// <summary>
    /// Specifies the standard buttons of the Force Feedback 2.
    /// </summary>
    public enum Button : uint
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
        /// The 7 button.
        /// </summary>
        Button7 = 0x0040,

        /// <summary>
        /// The 8 button.
        /// </summary>
        Button8 = 0x0080,
    }
}
