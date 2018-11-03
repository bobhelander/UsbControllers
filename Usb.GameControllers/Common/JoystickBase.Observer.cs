using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usb.Hid.Connection.models;

namespace Usb.GameControllers.Common
{
    public partial class JoystickBase<T> : IObserver<ReadBuffer>
    {
        public void OnNext(ReadBuffer value)
        {
            Notify(States.CreateFromBuffer(value.Buffer));
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
