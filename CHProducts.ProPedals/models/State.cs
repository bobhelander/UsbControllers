using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CHProducts.ProPedals.models
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
