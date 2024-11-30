using System.Collections;
using CommonVariables;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _trail;

        private Rigidbody _rigidbody;
        private Variables _variables;

        private void Awake()
        {
            _variables = new Variables();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot(Vector3 startPosition, Vector3 speed)
        {
            transform.position = startPosition;
            _rigidbody.velocity = speed * 10;
            _trail.Play();
        }

        public void SetActive()
        {
            gameObject.SetActive(true);
            StartCoroutine(Die());
        }

        public void Died()
        {
            _trail.Stop();
            gameObject.SetActive(false);
        }

        private IEnumerator Die()
        {
            while (enabled)
            {
                yield return _variables.Delay;
                Died();
            }
        }
    }
}