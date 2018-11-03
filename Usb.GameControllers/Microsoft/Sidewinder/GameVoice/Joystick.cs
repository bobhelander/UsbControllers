using Usb.GameControllers.Common;
using Usb.GameControllers.Microsoft.Sidewinder.GameVoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.Microsoft.Sidewinder.GameVoice
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Sidewinder Game Voice device.
        /// </summary>
        public const int VendorId = 0x045e;

        /// <summary>
        /// The product id for the Sidewinder Game Voice device.
        /// </summary>
        public const int ProductId = 0x003B;

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

        /// <summary>
        /// Defines the enabled lights on the controller.
        /// </summary>
        public byte Lights
        {
            get { return Controller.FeatureValue[1]; }
            set { Controller.FeatureValue = new byte[] { 0x00, value }; }
        }
    }
}
