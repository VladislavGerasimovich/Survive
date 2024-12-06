using System;
using Game.Buttons;
using TMPro;
using UnityEngine;

namespace Menu.DifficultyLevels
{
    [RequireComponent(typeof(PressButton))]
    public class DifficultyCard : MonoBehaviour
    {
        [SerializeField] private int _reward;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private int _level;

        private PressButton _pressButton;

        public event Action Click;

        private void Awake()
        {
            _pressButton = GetComponent<PressButton>();
            _text.text = _reward.ToString();
        }

        private void OnEnable()
        {
            _pressButton.Click += OnCardClick;
        }

        private void OnDisable()
        {
            _pressButton.Click -= OnCardClick;
        }

        private void OnCardClick()
        {
            Click?.Invoke();
            PlayerPrefs.SetInt(Constants.Level, _level);
            PlayerPrefs.SetInt(Constants.Reward, _reward);
            PlayerPrefs.Save();
        }
    }
}