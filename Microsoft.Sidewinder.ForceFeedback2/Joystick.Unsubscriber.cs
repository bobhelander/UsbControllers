using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.ForceFeedback2
{
    public partial class Joystick
    {
        private class Unsubscriber : IDisposable
        {
            private List<IObserver<States>> _observers;
            private IObserver<States> _observer;

            public Unsubscriber(List<IObserver<States>> observers, IObserver<States> observer)
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
