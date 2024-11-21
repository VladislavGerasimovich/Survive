using System;
using UnityEngine;

namespace Game.Collision
{
    public class CollisionHandler : MonoBehaviour
    {
        public virtual event Action<int> Collided;

        public virtual void OnTriggerEnter(Collider collision)
        { }
    }
}