using Usb.GameControllers.Common;
using Usb.GameControllers.LeoBodnar.BBI32.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
        /// <param name="logger">
        /// Microsoft.Extensions.Logging logger. Null to disable logging.
        /// </param>
        public Joystick(string devicePath, ILogger logger)
            : base(devicePath, logger)
        {
            Controller.ContinuousUsb = true;

            // Debounce the buttons
            Controller.ContinuousUsbDebounce = true;
            // Button data is bytes 1-4 
            Controller.ContinuousUsbDebounceButtonsIndex = 1;
        }
    }
}
