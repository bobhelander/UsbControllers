using System;

namespace Microsoft.Sidewinder.GameVoice.models
{
    /// <summary>
    /// Specifies the standard buttons of the Game Voice.
    /// </summary>
    public enum Button : byte
    {
        /// <summary>
        /// The All button.
        /// </summary>
        ButtonAll = 0x01,

        /// <summary>
        /// The Team button.
        /// </summary>
        ButtonTeam = 0x02,

        /// <summary>
        /// The 1 button.
        /// </summary>
        Button1 = 0x04,

        /// <summary>
        /// The 2 button.
        /// </summary>
        Button2 = 0x08,

        /// <summary>
        /// The 3 button.
        /// </summary>
        Button3 = 0x10,

        /// <summary>
        /// The 4 button.
        /// </summary>
        Button4 = 0x20,

        /// <summary>
        /// The Command button.
        /// </summary>
        CommandButton = 0x40,

        /// <summary>
        /// The Mute button.
        /// </summary>
        MuteButton = 0x80,
    }
}
