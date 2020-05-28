using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.ObjectPool
{
    interface IObjectPoolManager
    {
        GameObject Spawn(GameObject prefab);
        bool Recycle(GameObject poolObj);
        bool Recycle(BasePoolObj poolObj);
        void Clear();
    }
}