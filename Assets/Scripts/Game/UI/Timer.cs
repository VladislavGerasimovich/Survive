using System.Collections;
using Game.UI.Screens;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _remainigTime;
        [SerializeField] private ContinueGamePanel _continueGamePanel;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private float _extraTime;
        [SerializeField] private int _countOfExtraLevels;
        [SerializeField] private Reward.Reward _reward;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            StartCoroutine(Run());
        }

        public void SetRemainingTime()
        {
            _remainigTime = _extraTime;
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            int minutes;
            int seconds;

            while (_remainigTime > 1)
            {
                _remainigTime -= Time.deltaTime;
                minutes = Mathf.FloorToInt(_remainigTime / 60);
                seconds = Mathf.FloorToInt(_remainigTime % 60);
                _text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                yield return null;
            }

            _reward.Add();

            _countOfExtraLevels--;

            if (_countOfExtraLevels < 0)
            {
                _gameOverPanel.Open();
                yield break;
            }

            _continueGamePanel.Open();
        }
    }
}