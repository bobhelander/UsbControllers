using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Usb.GameControllers.CHProducts.ProPedals.Models
{
    /// <summary>
    /// Represents the state of a CH Pedals Controller.
    /// </summary>
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
        /// Gets or sets the rotation of the controller.
        /// </summary>
        public int R { get; set; }

        /// <summary>
        /// Gets of sets the pressed buttons.  None on this controller
        /// </summary>
        public UInt32 Buttons { get; set; } = 0;

        /// <summary>
        /// Creates a <see cref="State"/> from the output bytes of the controller.
        /// </summary>
        internal static State Create(byte[] values)
        {
            return new State()
            {
                X = (int)values[1],
                Y = (int)values[2],
                R = (int)values[3],
            };
        }
    }
}
