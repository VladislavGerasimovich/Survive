using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private ExplosionArea _explosionArea;
        [SerializeField] private GameObject _grenadePrefab;
        [SerializeField] private GrenadeCollisionHandler _collisionHandler;

        private AudioSource _explosionSound;

        private void Awake()
        {
            _explosionSound = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _collisionHandler.TouchedGround += Explosion;
        }

        private void OnDisable()
        {
            _collisionHandler.TouchedGround -= Explosion;
        }

        public void ShowGrenadePrefab(Vector3 position, Vector3 force)
        {
            _grenadePrefab.SetActive(true);
            _grenadePrefab.transform.position = position;
            _grenadePrefab.GetComponent<Rigidbody>().velocity = force;
        }

        private void Explosion(Vector3 position)
        {
            _explosionArea.GetComponent<ExplosionArea>().Run(position);

            if (_explosionSound.enabled == true)
            {
                _explosionSound.Play();
            }
        }
    }
}