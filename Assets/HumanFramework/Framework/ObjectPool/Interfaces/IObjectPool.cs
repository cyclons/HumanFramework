using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HumanFramework.ObjectPool
{
    public interface IObjectPool
    {
        Stack<GameObject> PoolObjs { get; set; }
        GameObject Prefab { get; set; }

        GameObject Spawn();
        void Register(GameObject prefab);
        bool Recycle(GameObject poolObj);
        void Clear();
    }
}
