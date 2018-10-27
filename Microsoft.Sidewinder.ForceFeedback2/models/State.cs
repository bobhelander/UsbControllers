using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.ForceFeedback2.models
{
    public class State : IState
    {
        /// <summary>
        /// An empty status.
        /// </summary>
        public static readonly State Empty = new State() { };

        /// <summary>
        /// Gets or sets the x-axis position of the controller.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y-axis position of the controller.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the rotation (twist) of the controller.
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the controller.
        /// </summary>
        public int Slider { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the controller.
        /// </summary>
        public int Hat { get; set; }

        /// <summary>
        /// Gets or sets the button states controller.
        /// </summary>
        public uint Buttons { get; set; }

        /// <summary>
        /// Creates a <see cref="Swff2Status"/> from the output bytes of the controller.
        /// </summary>
        /// <param name="values">The output bytes of the controller.</param>
        /// <returns>The associated status.</returns>
        /// <remarks>
        /// The bytes of the <paramref name="values"/> parameter are in reverse order.
        /// </remarks>
        internal static State Create(byte[] values)
        {
            //   0  1  2  3  4  5  6  7    8        9        10       11
            // [ ?, X, X, Y, Y, R, S, HAT, BUTTONS, BUTTONS, BUTTONS, BUTTONS]            
            short x = BitConverter.ToInt16(values, 1);
            short y = BitConverter.ToInt16(values, 3);

            int rotation = (int)(sbyte)values[5];
            int slider = values[6];
            int hat = values[7];
            uint buttons = values[8];

            return new State()
            {
                X = x,
                Y = y,
                R = rotation,
                Slider = slider,
                Hat = hat,
                Buttons = buttons,
            };
        }
    }
}
