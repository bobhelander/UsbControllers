﻿using Joystick.Common;
using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.ForceFeedback2
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
        /// DirectX Product Guid for the Sidewinder Force Feedback 2
        /// </summary>
        public const string ProductGuid = "001b045e-0000-0000-0000-504944564944";

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
