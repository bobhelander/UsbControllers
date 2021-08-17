using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Usb.GameControllers.Common
{
    public partial class JoystickBase<T> : IDisposable where T : IStates , new()
    {
        /// <summary>
        /// Joystick current and previous states
        /// </summary>
        protected T States { get; set; } = new T();

        /// <summary>
        /// USB Connection to the device
        /// </summary>
        public Usb.Hid.Connection.Controller Controller { get; protected set; }

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
        /// <param name="logger">
        ///Microsoft.Extensions.Logging logger. Null to disable logging.
        /// </param>
        public JoystickBase(string devicePath, ILogger logger)
        {
            Controller = new Usb.Hid.Connection.Controller(devicePath, logger);
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
