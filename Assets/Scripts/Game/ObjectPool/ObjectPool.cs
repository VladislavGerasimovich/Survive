using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.ObjectPools
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] protected GameObject Container;
        [SerializeField] protected int Capacity;

        protected List<GameObject> Pool;

        protected virtual void Initialize(GameObject prefab)
        {
            for (int i = 0; i < Capacity; i++)
            {
                GameObject item = Instantiate(prefab, Container.transform);
                item.SetActive(false);
                Pool.Add(item);
            }
        }

        public virtual bool TryGetObject(out GameObject result)
        {
            result = Pool.FirstOrDefault(p => p.activeSelf == false);
            return result != null;
        }
    }
}