using Usb.GameControllers.Common;
using Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander 
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Strategic Commander device.
        /// </summary>
        public const int VendorId = 0x045e;

        /// <summary>
        /// The product id for the Strategic Commander device.
        /// </summary>
        public const int ProductId = 0x0033;

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="devicePath">
        /// The path of the device.
        /// </param>
        public Joystick(string devicePath)
            : base(devicePath)
        {
            Controller.EventsOnlyReported = true;
        }

        /// <summary>
        /// Get an input report from the device and place it into the input stream to process.  This is only done on initialization.
        /// </summary>
        public async Task ReadInputReportAsync()
        {
            await Controller.ProcessInputReport().ConfigureAwait(false);
        }

        /// <summary>
        /// Sets the enabled lights on the controller.
        /// </summary>
        /// <param name="lights">The enabled lights on the controller.</param>
        public void SetLights(IEnumerable<Light> lights)
        {
            short lightsValue = (short)lights.Sum(l => (int)l);
            byte[] values = new byte[Controller.FeatureLength];
            values[1] = (byte)(lightsValue & 0x00ff);
            values[2] = (byte)(lightsValue >> 8);
            Controller.Feature = values;
        }
    }
}
