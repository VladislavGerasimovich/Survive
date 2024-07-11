using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Audio))]
public class MelleWeaponCollisionHandler : MonoBehaviour
{
    private Audio _audio;

    private void Awake()
    {
        _audio = GetComponent<Audio>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out ZombieDied zombieDied))
        {
            _audio.Play();
        }
    }
}
