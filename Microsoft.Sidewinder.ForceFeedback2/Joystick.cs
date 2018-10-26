using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbController.models;

namespace Microsoft.Sidewinder.ForceFeedback2
{
    public partial class Joystick
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
        /// UBS Connection to the device
        /// </summary>
        private UsbController.Controller Controller { get; set; }

        /// <summary>
        /// Disconnect from the device
        /// </summary>
        private IDisposable ControllerUnsubscriber { get; set; }

        /// <summary>
        /// Previous and current state
        /// </summary>
        private States States { get; set; } = new States { Current = State.Empty, Previous = State.Empty };

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="devicePath">
        /// The path of the device.
        /// </param>
        public Joystick(string devicePath)
        {
            Controller = new UsbController.Controller(devicePath);
            ControllerUnsubscriber = Controller.Subscribe(this);
        }

        /// <summary>
        /// Initializes the controller for reads.
        /// </summary>
        public void Initialize()
        {
            Controller.Initialize();
        }
    }
}
