using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCollisionHandler : MonoBehaviour
{
    public event Action<Vector3> TouchedGround;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Plane plane))
        {
            TouchedGround?.Invoke(transform.position);
            gameObject.SetActive(false);
        }
    }
}
