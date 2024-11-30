using System;
using Game.Player;
using Game.Weapons.Damage;
using Game.Zombie;
using UnityEngine;

namespace Game.Collision
{
    public class ZombieCollisionHandler : CollisionHandler
    {
        [SerializeField] private ZombieAttack _zombieAttack;

        public override event Action<int> Collided;

        public override void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out WeaponDamage weaponDamage))
            {
                Collided?.Invoke(weaponDamage.Harm);
            }

            if (collision.TryGetComponent(out BulletDamage bulletDamage))
            {
                Collided?.Invoke(bulletDamage.Harm);
                bulletDamage.Collision();
            }

            if (collision.TryGetComponent(out PlayerTakeDamage playerTakeDamage))
            {
                _zombieAttack.StartAttackCoroutine(playerTakeDamage);
            }
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.TryGetComponent(out PlayerTakeDamage playerTakeDamage))
            {
                _zombieAttack.StartAttackCoroutine(playerTakeDamage);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerTakeDamage playerTakeDamage))
            {
                _zombieAttack.StopAttackCoroutine();
            }
        }
    }
}