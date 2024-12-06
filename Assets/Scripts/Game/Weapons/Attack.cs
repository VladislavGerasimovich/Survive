using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommonVariables;
using UnityEngine;

namespace Game.Weapons
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _smoke;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private GameObject _container;
        [SerializeField] private int _capacity;
        [SerializeField] private GameObject _prefab;

        private List<GameObject> _pool;
        private Variables _variables;
        private Coroutine _shootCoroutine;

        private void Awake()
        {
            _pool = new List<GameObject>();
            Initialize(_prefab);
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
                TryGet(out GameObject bullet);
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

        private void Initialize(GameObject prefab)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject item = Instantiate(prefab, _container.transform);
                item.SetActive(false);
                _pool.Add(item);
            }
        }

        private bool TryGet(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.activeSelf == false);
            return result != null;
        }
    }
}