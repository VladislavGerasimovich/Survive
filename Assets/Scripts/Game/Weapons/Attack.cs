using System.Collections;
using UnityEngine;
using CommonVariables;

namespace Game.Weapons
{
    [RequireComponent(typeof(Variables))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _smoke;
        [SerializeField] private AudioSource _audioSource;

        private Variables _variables;
        private BulletsCreator _bulletsCreator;
        private Coroutine _shootCoroutine;

        private void Awake()
        {
            _bulletsCreator = GameObject.Find("Setups").GetComponent<BulletsCreator>();
            _variables = GetComponent<Variables>();
        }

        public void Enable()
        {
            enabled = true;
            _shootCoroutine = StartCoroutine(Shoot());
        }

        public void Disable()
        {
            StopCoroutineShoot();
            enabled = false;
        }

        public void StopCoroutineShoot()
        {
            StopCoroutine(_shootCoroutine);
        }

        private IEnumerator Shoot()
        {
            while (enabled)
            {
                _bulletsCreator.TryGetObject(out GameObject bullet);
                bullet.GetComponent<Bullet>().SetActive();
                bullet.GetComponent<Bullet>().Shoot(_shootPoint.position, _shootPoint.transform.forward);
                _smoke.Play();

                if (_audioSource.enabled == true)
                {
                    _audioSource.Play();
                }

                yield return _variables.Delay;
            }
        }
    }
}