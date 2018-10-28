using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thrustmaster.Warthog.Throttle.models
{
    /// <summary>
    /// Light Intensity
    /// </summary>
    public enum Intensity : byte
    {
        OFF = 0x00,
        EXTRA_LOW = 0x01,
        LOW = 0x02,
        MED = 0x03,
        HIGH = 0x04,
        EXTRA_HIGH = 0x05,
    }

    /// <summary>
    /// Light on the device
    /// </summary>
    public enum Light : byte
    {
        /// <summary>
        /// Top round light
        /// /// </summary>
        LED1 = 0x04,

        /// <summary>
        /// Round light 2
        /// </summary>
        LED2 = 0x02,

        /// <summary>
        /// Round light 3
        /// </summary>
        LED3 = 0x10,

        /// <summary>
        /// Round light 4
        /// </summary>
        LED4 = 0x01,

        /// <summary>
        /// Bottom round light
        /// </summary>
        LED5 = 0x40,

        /// <summary>
        /// All other lights on the device
        /// </summary>
        LEDBacklight = 0x08,
    }
}
