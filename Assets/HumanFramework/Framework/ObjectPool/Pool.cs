using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanFramework.ObjectPool;

namespace HumanFramework
{
    public class Pool
    {
        private static IObjectPoolManager PoolManager = new ObjectPoolManager();

        public static GameObject Spawn(GameObject prefab)
        {
            return PoolManager.Spawn(prefab);
        }

        public static bool Recycle(GameObject poolObj)
        {
            return PoolManager.Recycle(poolObj);
        }

        public static bool Recycle(BasePoolObj poolObj)
        {
            return PoolManager.Recycle(poolObj);
        }

        public static void Clear()
        {
            PoolManager.Clear();
        }

    }
}
