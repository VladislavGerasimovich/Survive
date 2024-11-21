using System;
using UnityEngine;

namespace Game.Player
{
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
            TakeDamage.Invoke(damage);

            if (_audioSource.enabled == true)
            {
                _audioSource.Play();
            }
        }
    }
}