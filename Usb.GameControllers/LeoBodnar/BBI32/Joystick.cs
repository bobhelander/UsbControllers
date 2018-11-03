using Usb.GameControllers.Common;
using Usb.GameControllers.LeoBodnar.BBI32.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.LeoBodnar.BBI32
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Leo Bodnar BBI-32 device.
        /// </summary>
        public const int VendorId = 0x1DD2;

        /// <summary>
        /// The product id for the Leo Bodnar BBI-32 device.
        /// </summary>
        public const int ProductId = 0x1150;

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
