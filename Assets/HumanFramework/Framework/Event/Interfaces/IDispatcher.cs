using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HumanFramework
{
    public interface IDispatcher<T> : IDispatchble where T : ISignal
    {
        void AddListener(Action<T> method);
        void RemoveListener(Action<T> method);
        void DispathchSignal(T signal);
    }
}