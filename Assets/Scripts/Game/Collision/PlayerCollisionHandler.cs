using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : CollisionHandler
{
    public override event Action<int> Collided;

    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out ZombieDamagee zombieDamage))
        {
            Collided?.Invoke(zombieDamage.Damage);
        }
    }
}
