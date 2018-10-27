using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joystick.Common
{
    public partial class JoystickBase<T> : IDisposable where T : IStates , new()
    {
        /// <summary>
        /// Joystick current and previous states
        /// </summary>
        protected T States { get; set; } = new T();

        /// <summary>
        /// UBS Connection to the device
        /// </summary>
        protected UsbController.Controller Controller { get; set; }

        /// <summary>
        /// Disconnect from the device
        /// </summary>
        private IDisposable ControllerUnsubscriber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Joystick"/> class.
        /// </summary>
        /// <param name="devicePath">
        /// The path of the device.
        /// </param>
        public JoystickBase(string devicePath)
        {
            Controller = new UsbController.Controller(devicePath);
            ControllerUnsubscriber = Controller.Subscribe(this);
        }

        /// <summary>
        /// Initializes the controller for reads.
        /// </summary>
        public void Initialize()
        {
            States.Initialize();
            Controller.Initialize();
        }

        /// <summary>
        /// Shutdown
        /// </summary>
        public void Dispose()
        {
            ControllerUnsubscriber?.Dispose();
        }
    }
}
