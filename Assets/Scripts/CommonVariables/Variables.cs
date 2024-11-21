using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonVariables
{
    public class Variables : MonoBehaviour
    {
        [SerializeField] private bool _isPlaying;
        [SerializeField] private bool _canUse;

        private WaitForSeconds _delay;

        public void ChangeDelay(float value)
        {
            _delay = new WaitForSeconds(value);
        }

        public void ChangeIsPlaying(bool value)
        {
            _isPlaying = value;
        }

        public void ChangeCanUse(bool value)
        {
            _canUse = value;
        }

        public WaitForSeconds Delay => _delay;
        public bool IsPlaying => _isPlaying;
        public bool CanUse => _canUse;
    }
}