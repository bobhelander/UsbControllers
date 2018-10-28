using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thrustmaster.Warthog.Throttle.models
{
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
}
