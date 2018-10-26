using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbController.models;

namespace UsbController
{
    public partial class Controller
    {
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ReadBuffer>> _observers;
            private IObserver<ReadBuffer> _observer;

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
