using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.Event
{
    public class SignalContainer:ISignalContainer
    {
        private Dictionary<Type, IDispatchble> DispatcherDict = new Dictionary<Type, IDispatchble>();

        public void AddListener<T>(Action<T> method) where T : ISignal
        {
            var signalType = typeof(T);
            if (DispatcherDict.ContainsKey(signalType))
            {
                var dispatcher = DispatcherDict[signalType] as Dispatcher<T>;
                dispatcher.AddListener(method);
            }
            else
            {
                var dispatcher = new Dispatcher<T>();
                dispatcher.AddListener(method);
                DispatcherDict.Add(signalType, dispatcher);
            }
        }

        public void RemoveListener<T>(Action<T> method) where T : ISignal
        {
            if (DispatcherDict.ContainsKey(typeof(T)))
            {
                var dispatcher = DispatcherDict[typeof(T)] as Dispatcher<T>;
                dispatcher.RemoveListener(method);
            }
        }

        public void DispatchSignal<T>(T signal) where T : ISignal
        {
            if (DispatcherDict.ContainsKey(typeof(T)))
            {
                var dispatcher = DispatcherDict[typeof(T)] as Dispatcher<T>;
                dispatcher.DispathchSignal(signal);
            }
        }

        public void Clear()
        {
            DispatcherDict.Clear();
        }
    }
}
