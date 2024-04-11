using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ParticleSystem _smoke;

    private BulletsCreator _bulletsCreator;
    private WaitForSeconds _delay;
    private Coroutine _shootCoroutine;

    private void Awake()
    {
        _bulletsCreator = GameObject.Find("Setups").GetComponent<BulletsCreator>();
        _delay = new WaitForSeconds(0.75f);
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

            yield return _delay;
        }
    }
}
