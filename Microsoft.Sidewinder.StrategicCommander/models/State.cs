using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.StrategicCommander.models
{
    public class State : IState
    {
        /// <summary>
        /// An empty status.
        /// </summary>
        public static readonly State Empty = new State() { DownButtons = new Button[0] };

        /// <summary>
        /// Get the enumeration of the buttons into something we can use
        /// </summary>
        public static List<Button> CommanderButtons = Enum.GetValues(typeof(Button)).Cast<Button>().Select(x => x).ToList();

        /// <summary>
        /// Gets or sets the selected status.
        /// </summary>
        public int Profile { get; set; }

        /// <summary>
        /// Gets or sets the x-axis position of the controller.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-axis position of the controller.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the controller.
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Gets of sets the pressed buttons.
        /// </summary>
        public Button[] DownButtons { get; set; }

        /// <summary>
        /// Gets or sets the button states of the controller.
        /// </summary>
        public uint Buttons { get; set; }

        /// <summary>
        /// Creates a <see cref="SwscStatus"/> from the output bytes of the controller.
        /// </summary>
        /// <param name="values">The output bytes of the controller.</param>
        /// <returns>The associated status.</returns>
        /// <remarks>
        /// The bytes of the <paramref name="values"/> parameter are in reverse order.
        /// </remarks>
        internal static State Create(byte[] values)
        {
            // values[] :   6  5  4  3  2  1  0
            // reserved : [00 00 00 00 00 00 ff]
            // x        : [00 00 00 00 03 ff 00]
            // y        : [00 00 00 0f fc 00 00]
            // r        : [00 00 3f f0 00 00 00]
            // buttons  : [0f ff 00 00 00 00 00]
            // profile  : [30 00 00 00 00 00 00]
            int x = ((values[2] & 0x03) << 8) | values[1];
            int y = ((values[3] & 0x0f) << 6) | (values[2] >> 2);
            int r = (values[4] << 4) | (values[3] >> 4);
            int b = ((values[6] & 0x0f) << 8) | values[5];
            int p = values[6] >> 4;

            // Retrieve pressed buttons by comparing the bits
            var buttons = CommanderButtons.Where(cb => ((b & (int)cb) == (int)cb)).ToArray();

            // Return result with a final correction on (x, y, r) to manage negative figures (two-components encoding)
            return new State()
            {
                Profile = p,
                X = (0x200 & x) == 0x200 ? (x & 0x1ff) - 0x200 : x,
                Y = (0x200 & y) == 0x200 ? (y & 0x1ff) - 0x200 : y,
                R = (0x200 & r) == 0x200 ? (r & 0x1ff) - 0x200 : r,
                DownButtons = buttons,
                Buttons = (uint)b,
            };
        }
    }
}
