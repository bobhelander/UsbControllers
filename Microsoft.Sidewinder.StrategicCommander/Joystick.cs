using Joystick.Common;
using Microsoft.Sidewinder.StrategicCommander.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.StrategicCommander 
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Sidewinder Force Feedback 2 device.
        /// </summary>
        public const int VendorId = 0x045e;

        /// <summary>
        /// The product id for the Sidewinder Force Feedback 2 device.
        /// </summary>
        public const int ProductId = 0x001b;

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
