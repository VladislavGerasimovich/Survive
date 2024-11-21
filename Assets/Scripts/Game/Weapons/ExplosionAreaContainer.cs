using System.Collections.Generic;
using UnityEngine;
using Game.Weapons.Damage;

namespace Game.Weapons
{
    public class ExplosionAreaContainer : BulletsContainer
    {
        private List<WeaponDamage> _weaponDamage;

        private void Awake()
        {
            _weaponDamage = new List<WeaponDamage>();
        }

        public override void SetPoolObjectDamage(int value)
        {
            foreach (WeaponDamage grenade in _weaponDamage)
            {
                grenade.Change(value);
            }
        }

        public override void FindPoolObjects()
        {
            foreach (Transform child in transform)
            {
                _weaponDamage.Add(child.GetChild(0).GetComponent<WeaponDamage>());
            }
        }
    }
}