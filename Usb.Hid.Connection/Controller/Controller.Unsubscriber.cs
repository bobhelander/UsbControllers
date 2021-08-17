using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usb.Hid.Connection.models;

namespace Usb.Hid.Connection
{
    public partial class Controller
    {
        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<ReadBuffer>> _observers;
            private readonly IObserver<ReadBuffer> _observer;

            public Unsubscriber(List<IObserver<ReadBuffer>> observers, IObserver<ReadBuffer> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
