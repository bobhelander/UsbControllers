using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.Microsoft.Sidewinder.GameVoice.Models
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
