using System;
using System.Collections.Generic;
using System.Text;
using Usb.GameControllers.Interfaces;

namespace Usb.GameControllers.Common
{
    public static class Reactive
    {
        public static bool ButtonsChanged(IStates value)
        {
            return value.Current.Buttons != value.Current.Buttons;
        }

        public static bool ButtonDown(IStates value, UInt32 button)
        {
            return (value.Current.Buttons & button) == button;
        }

        public static bool ButtonPressed(IStates value, UInt32 button)
        {
            return ((value.Previous.Buttons & button) == 0 && (value.Current.Buttons & button) == button);
        }

        public static bool ButtonReleased(IStates value, UInt32 button)
        {
            return ((value.Previous.Buttons & button) == button && (value.Current.Buttons & button) == 0);
        }

        public static bool ButtonPressedOrReleased(IStates value, UInt32 button)
        {
            return (ButtonReleased(value, button) || ButtonPressed(value, button));
        }
    }
}
