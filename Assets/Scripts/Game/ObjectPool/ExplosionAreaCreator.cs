using System.Collections.Generic;
using UnityEngine;

namespace Game.ObjectPools
{
    public class ExplosionAreaCreator : ObjectPool
    {
        [SerializeField] private GameObject _explosionArea;

        private void Awake()
        { }

        public void Run()
        {
            Pool = new List<GameObject>();
            Initialize(_explosionArea);
        }
    }
}