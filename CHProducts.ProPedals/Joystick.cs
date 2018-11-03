using CHProducts.ProPedals.models;
using Joystick.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHProducts.ProPedals
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the CH Pedals device.
        /// </summary>
        public const int VendorId = 0x068e;

        /// <summary>
        /// The product id for the CH Pedals device.
        /// </summary>
        public const int ProductId = 0xc501;

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="devicePath">
        /// The path of the device.
        /// </param>
        public Joystick(string devicePath) 
            : base(devicePath)
        {
        }
    }
}
