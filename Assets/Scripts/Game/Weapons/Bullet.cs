using System.Collections;
using UnityEngine;
using CommonVariables;

namespace Game.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Variables))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _trail;

        private Rigidbody _rigidbody;
        private Variables _variables;

        private void Awake()
        {
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