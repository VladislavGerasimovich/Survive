using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerTakeDamage : MonoBehaviour
{
    private AudioSource _audioSource;

    public event Action<int> TakeDamage;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Take(int damage)
    {
        Debug.Log("Take damage + audio punch");
        TakeDamage.Invoke(damage);

        if(_audioSource.enabled == true)
        {
            _audioSource.Play();
        }
    }
}
