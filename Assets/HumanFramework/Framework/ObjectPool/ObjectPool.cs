using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HumanFramework.ObjectPool
{
    class ObjectPool :  IObjectPool
    {
        public Stack<GameObject> PoolObjs { get; set; }
        public GameObject Prefab { get; set; }

        public ObjectPool(GameObject prefab)
        {
            PoolObjs = new Stack<GameObject>();
            Register(prefab);
        }

        public bool Recycle(GameObject poolObj)
        {
            if (poolObj == null) { return false; }
            var poolScript = poolObj.GetComponent<IPoolable>();
            if (poolScript == null) { return false; }
            if (!poolScript.Active) { return false; }
            PoolObjs.Push(poolObj);
            poolScript.OnRecycle();
            return true;
        }

        public void Register(GameObject prefab)
        {
            Prefab=prefab;
        }

        public GameObject Spawn()
        {
            GameObject requiredObj;
            IPoolable poolScript;
            if (PoolObjs.Count == 0)
            {
                requiredObj = GameObject.Instantiate(Prefab);
                poolScript = requiredObj.GetComponent<IPoolable>();
                if (poolScript == null)
                {
                    poolScript = requiredObj.AddComponent<BasePoolObj>();
                    poolScript.RecycleToPool += Recycle;
                }
            }
            else
            {
                requiredObj = PoolObjs.Pop();
                poolScript = requiredObj.GetComponent<IPoolable>();
            }
            poolScript.OnSpawn();
            return requiredObj;
        }

        public void Clear()
        {
            while (PoolObjs.Count > 0)
            {
                GameObject.Destroy(PoolObjs.Pop());
            }
        }
    }
}
