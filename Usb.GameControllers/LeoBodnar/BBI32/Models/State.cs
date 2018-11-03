using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Usb.GameControllers.LeoBodnar.BBI32.Models
{
    /// <summary>
    /// Represents the state of a BBI-32 Controller
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// An empty status.
        /// </summary>
        public static readonly State Empty = new State() { };
        
        /// <summary>
        /// Gets or sets the button states controller.
        /// </summary>
        public uint Buttons { get; set; }

        /// <summary>
        /// Creates a <see cref="State"/> from the output bytes of the controller.
        /// </summary>
        internal static State Create(byte[] values)
        {            
            uint buttons = BitConverter.ToUInt32(values, 1);

            return new State()
            {                
                Buttons = buttons,
            };
        }
    }
}
