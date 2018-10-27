﻿using Joystick.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrustmaster.Warthog.Throttle.models;
using UsbController.models;

namespace Thrustmaster.Warthog.Throttle
{
    public partial class Joystick : JoystickBase<States>
    {
        /// <summary>
        /// The vendor id for the Thrustmaster Warthog Throttle device.
        /// </summary>
        public const int VendorId = 0x044f;

        /// <summary>
        /// The product id for the Thrustmaster Warthog Throttle device.
        /// </summary>
        public const int ProductId = 0x0404;

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
        /// Controls the LED lights on the controller
        /// </summary>
        public byte LightIntensity
        {
            get { return ((State)States.Current).LightIntensity; }
            set { UpdateLights(Lights, value).Wait(); }
        }

        /// <summary>
        /// Controls the LED lights on the controller
        /// </summary>
        public byte Lights
        {
            get { return ((State)States.Current).Lights; }
            set { UpdateLights(value, LightIntensity).Wait(); }
        }

        /// <summary>
        /// Controls the LED lights on the controller
        /// </summary>
        public async Task UpdateLights(byte lights, byte lightIntensity)
        {
            byte[] buffer = new byte[Controller.WriteLength];
            buffer[0] = 0x01;
            buffer[1] = 0x06;
            buffer[2] = lights;
            buffer[3] = lightIntensity;

            await Controller.Write(buffer, Controller.WriteLength);
        }
    }
}
