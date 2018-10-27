using System;

namespace Thrustmaster.Warthog.Throttle.models
{
    /// <summary>
    /// Specifies the standard buttons of the Thrustmaster Warthog
    /// </summary>
    public enum Button : UInt32
    {
        /// <summary>
        /// The 1 button.
        /// </summary>
        Button01 = 0x00000001,

        /// <summary>
        /// The 2 button.
        /// </summary>
        Button02 = 0x00000002,

        /// <summary>
        /// The 3 button.
        /// </summary>
        Button03 = 0x00000004,

        /// <summary>
        /// The 4 button.
        /// </summary>
        Button04 = 0x00000008,

        /// <summary>
        /// The 5 button.
        /// </summary>
        Button05 = 0x00000010,

        /// <summary>
        /// The 6 button.
        /// </summary>
        Button06 = 0x00000020,

        /// <summary>
        /// The 7 button.
        /// </summary>
        Button07 = 0x00000040,

        /// <summary>
        /// The 8 button.
        /// </summary>
        Button08 = 0x00000080,

        /// <summary>
        /// The 9 button.
        /// </summary>
        Button09 = 0x00000100,

        /// <summary>
        /// The 10 button.
        /// </summary>
        Button10 = 0x00000200,

        /// <summary>
        /// The 11 button.
        /// </summary>
        Button11 = 0x00000400,

        /// <summary>
        /// The 12 button.
        /// </summary>
        Button12 = 0x00000800,

        /// <summary>
        /// The 13 button.
        /// </summary>
        Button13 = 0x00001000,

        /// <summary>
        /// The 14 button.
        /// </summary>
        Button14 = 0x00002000,

        /// <summary>
        /// The 15 button.
        /// </summary>
        Button15 = 0x00004000,

        /// <summary>
        /// The 16 button.
        /// </summary>
        Button16 = 0x00008000,

        /// <summary>
        /// The 17 button.
        /// </summary>
        Button17 = 0x00010000,

        /// <summary>
        /// The 18 button.
        /// </summary>
        Button18 = 0x00020000,

        /// <summary>
        /// The 19 button.
        /// </summary>
        Button19 = 0x00040000,

        /// <summary>
        /// The 20 button.
        /// </summary>
        Button20 = 0x00080000,

        /// <summary>
        /// The 21 button.
        /// </summary>
        Button21 = 0x00100000,

        /// <summary>
        /// The 22 button.
        /// </summary>
        Button22 = 0x00200000,

        /// <summary>
        /// The 23 button.
        /// </summary>
        Button23 = 0x00400000,

        /// <summary>
        /// The 24 button.
        /// </summary>
        Button24 = 0x00800000,

        /// <summary>
        /// The 25 button.
        /// </summary>
        Button25 = 0x01000000,

        /// <summary>
        /// The 26 button.
        /// </summary>
        Button26 = 0x02000000,

        /// <summary>
        /// The 27 button.
        /// </summary>
        Button27 = 0x04000000,

        /// <summary>
        /// The 28 button.
        /// </summary>
        Button28 = 0x08000000,

        /// <summary>
        /// The 29 button.
        /// </summary>
        Button29 = 0x10000000,

        /// <summary>
        /// The 30 button.
        /// </summary>
        Button30 = 0x20000000,

        /// <summary>
        /// The 31 button.
        /// </summary>
        Button31 = 0x40000000,

        /// <summary>
        /// The 32 button.
        /// </summary>
        Button32 = 0x80000000,
    }

    /// <summary>
    /// Special conditions with multiple state switches
    /// </summary>
    public enum SwitchNeutral : UInt32
    {
        /// <summary>
        /// Mic Switch
        /// </summary>
        MSNONE = 0x0000003E,

        /// <summary>
        /// Speed Brake
        /// </summary>
        SPDM = 0x000000C0, // Button07 & Button08

        /// <summary>
        /// Boat Switch
        /// </summary>
        BSM = 0x00000300, // Button09 & Button10

        /// <summary>
        /// China Hat.
        /// </summary>
        CHM = 0x00000C00,  // Button11 & Button12

        /// <summary>
        /// Pinky Switch
        /// </summary>
        PSM = 0x00003000,  // Button13 & Button14

        /// <summary>
        /// Engine Left
        /// </summary>
        EOLNORM = 0x00020000 | 0x40000000, // Button18 & Button31

        /// <summary>
        /// Engine Right
        /// </summary>
        EORNORM = 0x00040000 | 0x80000000, // Button19 & Button32

        /// <summary>
        /// Flaps
        /// </summary>
        FLAPM = 0x00600000, // Button22 & Button23

        /// <summary>
        /// Auto Pilot
        /// </summary>
        APAH = 0x0C000000 // Button27 & Button28
    }

    /// <summary>
    /// Special conditions with multiple state switches
    /// </summary>
    public enum HatSwitch
    {
        /// <summary>
        /// Auto Pilot
        /// </summary>
        UP = 0x10,

        /// <summary>
        /// Auto Pilot
        /// </summary>
        RIGHT = 0x20,

        /// <summary>
        /// Auto Pilot
        /// </summary>
        DOWN = 0x40,

        /// <summary>
        /// Auto Pilot
        /// </summary>
        LEFT = 0x80,
    }

    /// <summary>
    /// Light on the device
    /// </summary>
    public enum Lights
    {
        /// <summary>
        /// 
        /// </summary>
        LED4 = 0x01,
        
        /// <summary>
        /// 
        /// </summary>
        LED2 = 0x02,

        /// <summary>
        /// 
        /// </summary>
        LED1 = 0x04,

        /// <summary>
        /// 
        /// </summary>
        LEDBacklight = 0x08,

        /// <summary>
        /// 
        /// </summary>
        LED3 = 0x10,

        /// <summary>
        /// 
        /// </summary>
        LED5 = 0x40,
    }
}
