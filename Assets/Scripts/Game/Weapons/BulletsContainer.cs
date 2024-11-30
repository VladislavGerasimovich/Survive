using System.Collections.Generic;
using Game.Weapons.Damage;
using UnityEngine;

namespace Game.Weapons
{
    public class BulletsContainer : MonoBehaviour
    {
        private List<BulletDamage> _bulletDamages;

        private void Awake()
        {
            _bulletDamages = new List<BulletDamage>();
        }

        private void Start()
        {
            FindPoolObjects();
        }

        public virtual void SetPoolObjectDamage(int value)
        {
            foreach (BulletDamage bullet in _bulletDamages)
            {
                bullet.Change(value);
            }
        }

        public virtual void FindPoolObjects()
        {
            foreach (Transform child in transform)
            {
                _bulletDamages.Add(child.GetComponent<BulletDamage>());
            }
        }
    }
}