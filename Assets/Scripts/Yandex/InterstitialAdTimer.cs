using System.Collections;
using UnityEngine;

namespace YandexElements
{
    public class InterstitialAdTimer : MonoBehaviour
    {
        private const int MaxValue = 60;

        private float _currentValue;
        private int _numberOfAdImpressions;
        private Coroutine _RunCoroutine;

        public bool IsReached { get; private set; }

        private void Awake()
        {
            _numberOfAdImpressions = PlayerPrefs.GetInt("NumberOfAdImpressions", 0);
        }

        private void Start()
        {
            _currentValue = PlayerPrefs.GetFloat("TimerValue", 0);

            if (_currentValue >= MaxValue)
            {
                _currentValue = 0;
            }

            if (_numberOfAdImpressions > 0)
            {
                _RunCoroutine = StartCoroutine(Run());
            }
        }

        private void OnDisable()
        {
            if (_RunCoroutine != null)
            {
                StopCoroutine(_RunCoroutine);
            }

            PlayerPrefs.SetFloat("TimerValue", _currentValue);
            PlayerPrefs.Save();
        }

        private IEnumerator Run()
        {
            while (_currentValue < MaxValue)
            {
                _currentValue += Time.deltaTime;

                yield return null;
            }

            if (_currentValue >= MaxValue)
            {
                IsReached = true;
            }
        }
    }
}