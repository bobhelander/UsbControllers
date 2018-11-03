using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Usb.GameControllers.Microsoft.Sidewinder.GameVoice.Models
{
    /// <summary>
    /// Represents the state of a Strategic Commander.
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// An empty status.
        /// </summary>
        public static readonly State Empty = new State() { DownButtons = new Button[0] };

        /// <summary>
        /// Get the enumeration of the buttons into something we can use
        /// </summary>
        public static List<Button> GameVoiceButtons = Enum.GetValues(typeof(Button)).Cast<Button>().Select(x => x).ToList();

        /// <summary>
        /// Gets of sets the pressed buttons.
        /// </summary>
        public Button[] DownButtons { get; set; }

        /// <summary>
        /// Gets of sets the pressed buttons.
        /// </summary>
        public byte Buttons { get; set; }

        /// <summary>
        /// Creates a <see cref="SwgvStatus"/> from the output bytes of the controller.
        /// </summary>
        /// <param name="values">The output bytes of the controller.</param>
        /// <returns>The associated status.</returns>
        /// <remarks>
        /// The bytes of the <paramref name="values"/> parameter are in reverse order.
        /// </remarks>
        internal static State Create(byte[] values)
        {
            // https://sourceforge.net/projects/ts3gamevoice/

            //enum Command { NONE = 0, ALL = 1, TEAM = 2, CHANNEL_1 = 4, CHANNEL_2 = 8, CHANNEL_3 = 16, CHANNEL_4 = 32, COMMAND = 64, MUTE = 128 };
            //enum Action { DEACTIVATED = 1024, ACTIVATED = 2048 };

            byte b = values[1];

            // Retrieve pressed buttons by comparing the bits
            var buttons = GameVoiceButtons.Where(gvb => ((b & (int)gvb) == (int)gvb)).ToArray();

            return new State {
                Buttons = b,
                DownButtons = buttons,
            };
        }
    }
}
