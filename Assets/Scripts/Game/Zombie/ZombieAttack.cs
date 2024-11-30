using System.Collections;
using CommonVariables;
using Game.Player;
using UnityEngine;

namespace Game.Zombie
{
    public class ZombieAttack : MonoBehaviour
    {
        [SerializeField] private ZombieDamage _zombieDamage;

        private Variables _variables;
        private Coroutine _attackCoroutine;

        private void Awake()
        {
            _variables = new Variables();
        }

        public void StartAttackCoroutine(PlayerTakeDamage playerTakeDamage)
        {
            if (_attackCoroutine == null)
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

                yield return _variables.Delay;
            }
        }
    }
}