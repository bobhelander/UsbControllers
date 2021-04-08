using Usb.GameControllers.CHProducts.ProPedals.Models;
using Usb.GameControllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.CHProducts.ProPedals
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
        /// The product id if the CH driver is not installed
        /// </summary>
        public const int AltProductId = 0x0501;

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
