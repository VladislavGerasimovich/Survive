using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAreaCreator : ObjectPool
{
    [SerializeField] private GameObject _explosionArea;

    private void Awake()
    {
    }

    public void Run()
    {
        _pool = new List<GameObject>();
        Initialize(_explosionArea);
    }
}
