using System.Collections;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private ZombieDamagee _zombieDamage;

    private WaitForSeconds _delay;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _delay = new WaitForSeconds(2);
    }

    public void StartAttackCoroutine(PlayerTakeDamage playerTakeDamage)
    {
        if(_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(Attack(playerTakeDamage));
        }
    }

    public void StopAttackCoroutine()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private IEnumerator Attack(PlayerTakeDamage playerTakeDamage)
    {
        while (enabled)
        {
            playerTakeDamage.Take(_zombieDamage.Harm);

            yield return _delay;
        }
    }
}
