using Microsoft.Sidewinder.ForceFeedback2.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Sidewinder.ForceFeedback2
{
    public partial class Joystick : IObservable<States>
    {
        private List<IObserver<States>> observers = new List<IObserver<States>>();

        public IDisposable Subscribe(IObserver<States> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        private void Notify(State state)
        {
            States.Previous = States.Current;
            States.Current = state;

            foreach (var observer in observers)
                observer.OnNext(States);
        }
    }
}
