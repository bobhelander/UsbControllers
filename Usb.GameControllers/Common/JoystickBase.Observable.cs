using Usb.GameControllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.GameControllers.Common
{
    public partial class JoystickBase<T> : IObservable<T> where T : IStates, new()
    {
        private List<IObserver<T>> observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new Unsubscriber(observers, observer);
        }

        private void Notify(IState state)
        {
            States.Previous = States.Current;
            States.Current = state;

            foreach (var observer in observers)
                observer.OnNext(States);
        }
    }
}
