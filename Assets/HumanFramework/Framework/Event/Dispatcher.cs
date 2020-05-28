using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.Event
{
    public class Dispatcher<T> : IDispatcher<T> where T : ISignal
    {

        private Action<T> SubscribedMethods = obj => { };

        public void AddListener(Action<T> method)
        {
            SubscribedMethods += method;
        }

        public void RemoveListener(Action<T> method)
        {
            SubscribedMethods -= method;
        }

        public void DispathchSignal(T signal)
        {
            SubscribedMethods.Invoke(signal);
        }

    }
}
