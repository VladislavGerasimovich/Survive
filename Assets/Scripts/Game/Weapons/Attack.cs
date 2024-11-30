using System.Collections;
using CommonVariables;
using UnityEngine;

namespace Game.Weapons
{
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
            _variables = new Variables();
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
                _bulletsCreator.TryGet(out GameObject bullet);
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