using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public virtual event Action<int> Collided;

    public virtual void OnTriggerEnter(Collider collision)
    {
    }
}
