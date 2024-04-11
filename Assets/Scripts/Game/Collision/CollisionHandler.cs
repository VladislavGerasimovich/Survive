using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public virtual event Action<int> Collided;

    public virtual void OnTriggerEnter(Collider collision)
    {
    }
}
