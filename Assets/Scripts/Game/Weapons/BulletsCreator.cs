using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsCreator : BulletsPool
{
    [SerializeField] private GameObject _prefab;

    private void Awake()
    {
        _pool = new List<GameObject>();
        Initialize(_prefab);
    }
}
