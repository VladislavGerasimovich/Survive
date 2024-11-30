using Agava.YandexGames;
using Menu.Shop;
using UnityEngine;

namespace Game.Buttons
{
    [RequireComponent(typeof(PressButton))]
    public class ButtonAd : MonoBehaviour
    {
        private const string English = "en";
        private const string Turkish = "tr";
        private const string Russian = "ru";

        [SerializeField] private YandexElements.VideoAd _videoAd;
        [SerializeField] private GameTime _gameTime;
        [SerializeField] private PopUpWindow _popUpWindow;
        [SerializeField] private string _englishText;
        [SerializeField] private string _turkishText;
        [SerializeField] private string _russianText;

        private PressButton _pressButton;
        private string _translatedText;

        private void Awake()
        {
            _pressButton = GetComponent<PressButton>();
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            ChangeLanguage(languageCode);
        }

        private void OnEnable()
        {
            _popUpWindow.YesButtonClicked += OnYesButtonClicked;
            _popUpWindow.NoButtonClicked += OnNoButtonClicked;
            _pressButton.Click += ShowPopUpWindow;
        }

        private void OnDisable()
        {
            _popUpWindow.YesButtonClicked -= OnYesButtonClicked;
            _popUpWindow.NoButtonClicked -= OnNoButtonClicked;
            _pressButton.Click -= ShowPopUpWindow;
        }

        public void ChangeLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case English:
                    _translatedText = _englishText;
                    break;
                case Turkish:
                    _translatedText = _turkishText;
                    break;
                case Russian:
                    _translatedText = _russianText;
                    break;
            }
        }

        private void ShowPopUpWindow()
        {
            _gameTime.Stop();
            _popUpWindow.Open(_translatedText);
        }

        private void OnYesButtonClicked()
        {
            _videoAd.Show();
        }

        private void OnNoButtonClicked()
        {
            _gameTime.Run();
        }
    }
}