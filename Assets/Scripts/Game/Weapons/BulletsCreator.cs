using System.Collections.Generic;
using UnityEngine;
using Game.ObjectPools;

namespace Game.Weapons
{
    public class BulletsCreator : BulletsPool
    {
        [SerializeField] private GameObject _prefab;

        private void Awake()
        {
            Pool = new List<GameObject>();
            Initialize(_prefab);
        }
    }
}