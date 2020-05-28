using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HumanFramework.ObjectPool
{
    public class ObjectPoolManager : IObjectPoolManager
    {
        public Dictionary<int, IObjectPool> PoolDict = new Dictionary<int, IObjectPool>();

        public bool Recycle(GameObject poolObj)
        {
            if (poolObj == null) return false;
            var poolScript = poolObj.GetComponent<IPoolable>();
            if (poolScript != null)
            {
                poolScript.RecycleToPool(poolObj);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Recycle(BasePoolObj poolObj)
        {
            if (poolObj == null) return false;
            poolObj.Recycle();
            return true;
        }

        public GameObject Spawn(GameObject prefab)
        {
            var poolID = GetPoolID(prefab);
            if (!PoolDict.ContainsKey(poolID))
            {
                PoolDict.Add(poolID, new ObjectPool(prefab));
            }

            return PoolDict[poolID].Spawn();
        }

        private int GetPoolID(GameObject prefab)
        {
            return prefab.GetInstanceID();
        }

        public void Clear()
        {
            foreach(var pool in PoolDict.Values)
            {
                pool.Clear();
            }
        }
    }
}
