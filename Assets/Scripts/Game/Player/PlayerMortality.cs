using System;
using System.Collections;
using UnityEngine;
using UI;
using CommonVariables;

namespace Game.Player
{
    [RequireComponent(typeof(Variables))]
    [RequireComponent(typeof(PlayerBlink))]
    public class PlayerMortality : MonoBehaviour
    {
        [SerializeField] private TimeOfAction _timeOfAction;
        [SerializeField] private float _duration;

        private PlayerBlink _playerBlink;
        private Variables _variables;

        public event Action IsMortal;

        private void Awake()
        {
            _variables = GetComponent<Variables>();
            _playerBlink = GetComponent<PlayerBlink>();
        }

        public void AllowUse()
        {
            _variables.ChangeCanUse(true);
        }

        public void ProhibitUse()
        {
            _variables.ChangeCanUse(false);
        }

        public void RunImmortalityCoroutine()
        {
            StartCoroutine(Immortality());
        }

        private IEnumerator Immortality()
        {
            _playerBlink.StartBlinkCoroutine();
            _timeOfAction.StartRunCoroutine(_duration);
            float duration = _duration;

            while (duration >= 0)
            {
                if (_variables.CanUse == true)
                {
                    duration -= Time.fixedDeltaTime;
                }

                yield return null;
            }

            _playerBlink.StopBlinkCoroutine();
            IsMortal.Invoke();
        }
    }
}