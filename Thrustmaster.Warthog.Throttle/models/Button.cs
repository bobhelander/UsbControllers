using System;

namespace Thrustmaster.Warthog.Throttle.models
{
    /// <summary>
    /// Specifies the standard buttons of the Thrustmaster Warthog
    /// </summary>
    public enum Button : UInt32
    {
        /// <summary>
        /// Slew Control Press (Mouse Pointinng Stick)
        /// </summary>
        Button01 = 0x00000001,

        /// <summary>
        /// MIC Switch Press
        /// </summary>
        Button02 = 0x00000002,

        /// <summary>
        /// MIC Switch Up
        /// </summary>
        Button03 = 0x00000004,

        /// <summary>
        /// MIC Switch Forward
        /// </summary>
        Button04 = 0x00000008,

        /// <summary>
        /// MIC Switch Down
        /// </summary>
        Button05 = 0x00000010,

        /// <summary>
        /// MIC Switch Back
        /// </summary>
        Button06 = 0x00000020,

        /// <summary>
        /// Speedbrake Forward
        /// </summary>
        Button07 = 0x00000040,

        /// <summary>
        /// Speedbrake Back
        /// </summary>
        Button08 = 0x00000080,

        /// <summary>
        /// Boat Switch Forward
        /// </summary>
        Button09 = 0x00000100,

        /// <summary>
        /// Boat Switch Back
        /// </summary>
        Button10 = 0x00000200,

        /// <summary>
        /// China Hat Forward
        /// </summary>
        Button11 = 0x00000400,

        /// <summary>
        /// China Hat Back
        /// </summary>
        Button12 = 0x00000800,

        /// <summary>
        /// Pinky Switch Forward
        /// </summary>
        Button13 = 0x00001000,

        /// <summary>
        /// Pinky Switch Back
        /// </summary>
        Button14 = 0x00002000,

        /// <summary>
        /// Left Tnhrottle Button (Autopilot Disengage)
        /// </summary>
        Button15 = 0x00004000,

        /// <summary>
        /// Engine Fuel Flow Left
        /// </summary>
        Button16 = 0x00008000,

        /// <summary>
        /// Engine Fuel Flow Right
        /// </summary>
        Button17 = 0x00010000,

        /// <summary>
        /// Engine Operate Left Down
        /// </summary>
        Button18 = 0x00020000,

        /// <summary>
        /// Engine Operate Right Down
        /// </summary>
        Button19 = 0x00040000,

        /// <summary>
        /// APU START
        /// </summary>
        Button20 = 0x00080000,

        /// <summary>
        /// Landing Gear Horn Silence (L/G WARN SILENCE)
        /// </summary>
        Button21 = 0x00100000,

        /// <summary>
        /// Flaps Up
        /// </summary>
        Button22 = 0x00200000,

        /// <summary>
        /// Flaps Down
        /// </summary>
        Button23 = 0x00400000,

        /// <summary>
        /// EAC 
        /// </summary>
        Button24 = 0x00800000,

        /// <summary>
        /// RDR ALTM
        /// </summary>
        Button25 = 0x01000000,

        /// <summary>
        /// Autopilot Engage/Disengage
        /// </summary>
        Button26 = 0x02000000,

        /// <summary>
        /// Autopilot Select Up
        /// </summary>
        Button27 = 0x04000000,

        /// <summary>
        /// Autopilot Select Down
        /// </summary>
        Button28 = 0x08000000,

        /// <summary>
        /// Right Engine Idle (Right Throttle Parked)
        /// </summary>
        Button29 = 0x10000000,

        /// <summary>
        /// Left Engine Idle (Left Throttle Parked)
        /// </summary>
        Button30 = 0x20000000,

        /// <summary>
        /// Engine Operate Left Up
        /// </summary>
        Button31 = 0x40000000,

        /// <summary>
        /// Engine Operate Right Up
        /// </summary>
        Button32 = 0x80000000,
    }
}
