﻿using Joystick.Common;
using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbController.models;

namespace Microsoft.Sidewinder.ForceFeedback2
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
        }
    }
}
