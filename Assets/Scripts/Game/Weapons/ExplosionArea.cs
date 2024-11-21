using System.Collections;
using UnityEngine;

namespace Game.Weapons
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class ExplosionArea : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosion;

        private CapsuleCollider _collider;

        private void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
        }

        public void Run(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = new Vector3(position.x, transform.position.y, position.z);
            _explosion.transform.position = position;
            _explosion.Play();
            StartCoroutine(LifeCircle());
        }

        private IEnumerator LifeCircle()
        {
            yield return new WaitForSeconds(1);
            _collider.enabled = false;

            yield return new WaitForSeconds(1.5f);
            transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _collider.enabled = true;
        }
    }
}