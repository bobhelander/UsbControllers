using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbController.models;

namespace Microsoft.Sidewinder.ForceFeedback2
{
    public partial class Joystick : IObserver<ReadBuffer>
    {
        public void OnNext(ReadBuffer value)
        {
            Notify(State.Create(value.Buffer));
        }

        public void OnError(Exception error)
        {
            foreach (var observer in observers)
                observer.OnError(error);
        }

        public void OnCompleted()
        {
            foreach (var observer in observers)
                observer.OnCompleted();
        }
    }
}
