using Usb.GameControllers.Common;
using Usb.GameControllers.LeoBodnar.BBI32.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.Pioneer.DDJSB2
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Pioneer DDJ-SB2 device.
        /// </summary>
        public const int VendorId = 0x2B73;

        /// <summary>
        /// The product id for the Pioneer DDJ-SB2 device.
        /// </summary>
        public const int ProductId = 0x0001;

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="devicePath">
        /// The path of the device.
        /// </param>
        public Joystick(string devicePath)
            : base(devicePath)
        {
            Controller.ContinuousUsb = false;

            // Debounce the buttons
            Controller.ContinuousUsbDebounce = false;
            // Button data is bytes 1-4 
            //Controller.ContinuousUsbDebounceButtonsIndex = 1;
        }
    }
}
