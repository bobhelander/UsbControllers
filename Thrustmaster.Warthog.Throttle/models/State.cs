using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Thrustmaster.Warthog.Throttle.models
{
    /// <summary>
    /// Represents the state of a Warthog.
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// An empty status.
        /// </summary>
        public static readonly State Empty = new State { };

        /// <summary>
        /// Gets or sets all the buttons.
        /// </summary>
        public UInt32 buttons { get; set; }

        /// <summary>
        /// Gets or sets the x-axis position of the controller.
        /// </summary>
        public short X { get; set; }

        /// <summary>
        /// Gets or sets the y-axis position of the controller.
        /// </summary>
        public short Y { get; set; }

        /// <summary>
        /// Gets or sets the Right Throttle of the controller.
        /// </summary>
        public ushort Z { get; set; }

        /// <summary>
        /// Gets or sets the Left Throttle of the controller.
        /// </summary>
        public ushort Zr { get; set; }

        /// <summary>
        /// Gets or sets the slider of the controller.
        /// </summary>
        public short Slider { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the controller. 15 None 0 = Up 3 = Right 5 = Down  7 = Left
        /// </summary>
        public int Hat { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the controller. 15 None 0 = Up 3 = Right 5 = Down  7 = Left
        /// </summary>
        public int HatSwitch { get; set; }

        /// <summary>
        /// Gets or sets the LEDs that are turned on
        /// </summary>
        public byte Lights { get; set; }

        /// <summary>
        /// Gets or sets the LED intensity  0 (off) to 5 (maximum brightness) 
        /// </summary>
        public byte LightIntensity { get; set; }

        /// <summary>
        /// Creates a <see cref="TmThrottleStatus"/> from the output bytes of the controller.
        /// </summary>
        /// <param name="values">The output bytes of the controller.</param>
        /// <returns>The associated status.</returns>
        /// <remarks>
        /// The bytes of the <paramref name="values"/> parameter are in reverse order.
        /// </remarks>
        internal static State Create(byte[] values)
        {
            // http://members.aon.at/mfranz/warthog.html

            UInt32 b = BitConverter.ToUInt32(values, 1);
            // 0xF0 is the Switches  0x0F is the location
            int hat = values[5] & 0x0F;
            int hatSwitch = values[5] & 0xF0;
            short x = BitConverter.ToInt16(values, 6);
            short y = BitConverter.ToInt16(values, 8);
            short slider = BitConverter.ToInt16(values, 10);
            ushort z = BitConverter.ToUInt16(values, 12);
            ushort zr = BitConverter.ToUInt16(values, 14);

            //short rawX = BitConverter.ToInt16(values, 16);
            //short rawY = BitConverter.ToInt16(values, 18);
            //short rawZ = BitConverter.ToInt16(values, 20);
            //short rawZr = BitConverter.ToInt16(values, 22);

            byte ledLights = values[26];
            byte ledBrightness = values[27];

            if (hat == 15)
                hat = 8; // Centered

            return new State()
            {
                buttons = b,
                Z = z,
                Zr = zr,
                Slider = slider,
                X = x,
                Y = y,
                Hat = hat,
                HatSwitch = hatSwitch,
                Lights = ledLights,
                LightIntensity = ledBrightness,
            };
        }
    }
}
