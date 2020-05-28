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
        public bool IsActive { get ; set ; }

        public virtual void OnSpawn()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public virtual void OnRecycle()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void Recycle()
        {
            RecycleToPool(this.gameObject);
        }
    }
}
