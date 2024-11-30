using System;
using System.Collections.Generic;
using Game.ObjectPools;
using Game.ObjectPools.ZombiePools;
using UnityEngine;

namespace Menu.Shop
{
    public class PopUpWindowForGame : PopUpWindow
    {
        [SerializeField] private ZombiesPools _zombiesPools;
        [SerializeField] private AudioSource _shotSound;
        [SerializeField] private GrenadesCreator _grenadesCreator;
        [SerializeField] private AudioSource _playerHurt;
        [SerializeField] private List<AudioSource> _woodBatsSound;

        public new bool IsOpen => _isOpen;

        public new event Action YesButtonClicked;
        public new event Action NoButtonClicked;

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            YesButton.onClick.AddListener(OnYesButtonClick);
            NoButton.onClick.AddListener(OnNoButtonClick);
        }

        private void OnDisable()
        {
            YesButton.onClick.RemoveListener(OnYesButtonClick);
            NoButton.onClick.RemoveListener(OnNoButtonClick);
        }

        public override void Open(string message = null)
        {
            YesButton.interactable = true;
            _isOpen = true;
            GameTime.Stop();
            CanvasGroup.alpha = 1;
            CanvasGroup.blocksRaycasts = true;
            _zombiesPools.PauseSound();
            _shotSound.Stop();
            _shotSound.volume = 0;
            _playerHurt.volume = 0;
            _grenadesCreator.PauseSound();

            foreach (AudioSource woodBatSound in _woodBatsSound)
            {
                woodBatSound.Stop();
                woodBatSound.volume = 0;
            }

            if (message != null)
            {
                Text.text = message;
            }
        }

        public override void Close()
        {
            _isOpen = false;
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
            GameTime.Run();
            _playerHurt.volume = 1;
            _shotSound.volume = 1;
            _zombiesPools.PlaySound();
            _grenadesCreator.PlaySound();

            foreach (AudioSource woodBatSound in _woodBatsSound)
            {
                woodBatSound.volume = 1;
            }
        }

        public override void OnYesButtonClick()
        {
            YesButton.interactable = false;
            YesButtonClicked.Invoke();
        }

        public override void OnNoButtonClick()
        {
            Close();
            NoButtonClicked.Invoke();
        }
    }
}