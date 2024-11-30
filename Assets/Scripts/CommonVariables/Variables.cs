using UnityEngine;

namespace CommonVariables
{
    public class Variables : MonoBehaviour
    {
        [SerializeField] private bool _isPlaying;
        [SerializeField] private bool _canUse;

        private WaitForSeconds _delay;
        private WaitForSeconds _durationOfReloading;

        public bool IsPlaying => _isPlaying;
        public bool CanUse => _canUse;
        public WaitForSeconds Delay => _delay;
        public WaitForSeconds DurationOfReloading => _durationOfReloading;

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

        public void ChangeDurationOfReloading(int value)
        {
            _durationOfReloading = new WaitForSeconds(value);
        }
    }
}