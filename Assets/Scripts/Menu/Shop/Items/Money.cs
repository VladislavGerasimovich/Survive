using System;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop.Items
{
    public class Money : Item
    {
        [SerializeField] private YandexElements.VideoAd _videoAd;

        private bool _isRewardReceived;

        public override event Action<string, string> Clicked;

        private void Awake()
        {
            Background = GetComponent<Image>();
            Button = GetComponent<Button>();
            Class = Type;
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            ChangeLanguage(languageCode);
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        public override void OnClick()
        {
            PopUpWindow.YesButtonClicked += Buy;
            PopUpWindow.NoButtonClicked += Cancel;
            PopUpWindow.Open(TranslatedText);
        }

        public void Buy()
        {
            PopUpWindow.YesButtonClicked -= Buy;
            PopUpWindow.NoButtonClicked -= Cancel;
            _videoAd.Show();
            _videoAd.OnRewardReceived += PrepareReward;
            _videoAd.OnCloseAd += AddReward;
        }

        private void Cancel()
        {
            PopUpWindow.YesButtonClicked -= Buy;
            PopUpWindow.NoButtonClicked -= Cancel;
            PopUpWindow.Close();
        }

        private void AddReward()
        {
            _videoAd.OnRewardReceived -= PrepareReward;
            _videoAd.OnCloseAd -= AddReward;

            if (_isRewardReceived == true)
            {
                _isRewardReceived = false;
                Clicked?.Invoke(Cost[0], Type);
            }
        }

        private void PrepareReward()
        {
            _isRewardReceived = true;
        }
    }
}