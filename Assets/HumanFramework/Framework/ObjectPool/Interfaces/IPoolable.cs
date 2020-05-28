using System;
using UnityEngine;

namespace HumanFramework.ObjectPool
{
    public interface IPoolable
    {
        Func<GameObject, bool> RecycleToPool { get; set; }

        bool IsActive { get; set; }

        void OnSpawn();

        void OnRecycle();
        
    }
}
