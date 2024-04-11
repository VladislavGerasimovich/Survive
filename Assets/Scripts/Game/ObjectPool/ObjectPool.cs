using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _container;
    [SerializeField] protected int _capacity;

    protected List<GameObject> _pool;

    protected virtual void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject item = Instantiate(prefab, _container.transform);
            item.SetActive(false);
            _pool.Add(item);
        }
    }

    public virtual bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }
}
