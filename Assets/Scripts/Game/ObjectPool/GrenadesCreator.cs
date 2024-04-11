using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadesCreator : ObjectPool
{
    [SerializeField] private GameObject _grenade;

    private void Awake()
    {
        _pool = new List<GameObject>();
        Initialize(_grenade);
    }

    protected override void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject item = Instantiate(prefab, _container.transform);
            item.SetActive(false);
            _pool.Add(item);
        }
    }
}
