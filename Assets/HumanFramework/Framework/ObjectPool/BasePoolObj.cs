using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.ObjectPool
{
    public class BasePoolObj : MonoBehaviour, IPoolable
    {
        public Func<GameObject,bool> RecycleToPool { get ; set ; }
        public bool Active { get ; set ; }

        public virtual void OnSpawn()
        {
            Active = true;
            gameObject.SetActive(true);
        }

        public virtual void OnRecycle()
        {
            Active = false;
            gameObject.SetActive(false);
        }

        public void Recycle()
        {
            RecycleToPool(this.gameObject);
        }
    }
}
