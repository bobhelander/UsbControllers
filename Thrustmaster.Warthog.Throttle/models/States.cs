using Joystick.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thrustmaster.Warthog.Throttle.models
{
    public class States : IStates
    {
        public IState Current { get; set; }
        public IState Previous { get; set; }

        public IState CreateFromBuffer(byte[] buffer)
        {
            return State.Create(buffer);
        }

        public void Initialize()
        {
            Current = State.Empty;
            Previous = State.Empty;
        }
    }
}
