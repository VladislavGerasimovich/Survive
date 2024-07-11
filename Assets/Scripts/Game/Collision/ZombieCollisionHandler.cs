using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieCollisionHandler : CollisionHandler
{
    [SerializeField] private ZombieAttack _zombieAttack;

    public override event Action<int> Collided;

    public override void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out WeaponDamage weaponDamage))
        {
            Collided?.Invoke(weaponDamage.Damage);
        }

        if (collision.TryGetComponent(out BulletDamage bulletDamage))
        {
            Collided?.Invoke(bulletDamage.Damage);
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
        if(other.TryGetComponent(out PlayerTakeDamage playerTakeDamage))
        {
            _zombieAttack.StopAttackCoroutine();
        }
    }
}
