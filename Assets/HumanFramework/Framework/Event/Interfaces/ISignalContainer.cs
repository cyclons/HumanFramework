using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework
{
    public interface ISignalContainer
    {
        void DispatchSignal<T>(T signal) where T : ISignal;

        void AddListener<T>(Action<T> method) where T : ISignal;

        void RemoveListener<T>(Action<T> method) where T : ISignal;

        void Clear();
    }
}
