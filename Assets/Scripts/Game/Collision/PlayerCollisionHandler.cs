using System;
using UnityEngine;
using Game.Zombie;

namespace Game.Collision
{
    public class PlayerCollisionHandler : CollisionHandler
    {
        public override event Action<int> Collided;

        public override void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out ZombieDamage zombieDamage))
            {
                Collided?.Invoke(zombieDamage.Harm);
            }
        }
    }
}