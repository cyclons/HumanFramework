using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.Event
{
    public class SignalContainer:ISignalContainer
    {
        private Dictionary<Type, IDispatchble> mDispatcherDict = new Dictionary<Type, IDispatchble>();
        
        public void AddListener<T>(Action<T> method) where T : ISignal
        {
            var signalType = typeof(T);
            if (mDispatcherDict.ContainsKey(signalType))
            {
                var dispatcher = mDispatcherDict[signalType] as Dispatcher<T>;
                dispatcher.AddListener(method);
            }
            else
            {
                var dispatcher = new Dispatcher<T>();
                dispatcher.AddListener(method);
                mDispatcherDict.Add(signalType, dispatcher);
            }
        }

        public void RemoveListener<T>(Action<T> method) where T : ISignal
        {
            if (mDispatcherDict.ContainsKey(typeof(T)))
            {
                var dispatcher = mDispatcherDict[typeof(T)] as Dispatcher<T>;
                dispatcher.RemoveListener(method);
            }
        }

        public void DispatchSignal<T>(T signal) where T : ISignal
        {
            if (mDispatcherDict.ContainsKey(typeof(T)))
            {
                var dispatcher = mDispatcherDict[typeof(T)] as Dispatcher<T>;
                dispatcher.DispathchSignal(signal);
            }
        }

        public void Clear()
        {
            mDispatcherDict.Clear();
        }
    }
}
