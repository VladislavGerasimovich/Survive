using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trail;
    private Rigidbody _rigidbody;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(4);
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
            yield return _delay;
            Died();
        }
    }
}
