using System.Collections.Generic;
using Game.ObjectPools;
using UnityEngine;

namespace Game.Weapons
{
    public class BulletsCreator : ObjectPool
    {
        [SerializeField] private GameObject _prefab;

        private void Awake()
        {
            Pool = new List<GameObject>();
            Initialize(_prefab);
        }
    }
}