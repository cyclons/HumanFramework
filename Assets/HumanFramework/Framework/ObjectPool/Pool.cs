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
        private static IObjectPoolManager mPoolManager = new ObjectPoolManager();

        public static GameObject Spawn(GameObject prefab)
        {
            return mPoolManager.Spawn(prefab);
        }

        public static bool Recycle(GameObject poolObj)
        {
            return mPoolManager.Recycle(poolObj);
        }

        public static bool Recycle(BasePoolObj poolObj)
        {
            return mPoolManager.Recycle(poolObj);
        }

        public static void Clear()
        {
            mPoolManager.Clear();
        }

    }
}
