using System.Collections;
using System.Collections.Generic;
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
            playerTakeDamage.Take(_zombieDamage.Damage);

            yield return _delay;
        }
    }
}
