using Game.Music;
using Game.Zombie;
using UnityEngine;

namespace Game.Weapons
{
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
            if (collision.TryGetComponent(out ZombieDeath zombieDied))
            {
                _audio.Play();
            }
        }
    }
}