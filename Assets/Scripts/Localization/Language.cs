using Agava.YandexGames;
using UnityEngine;

namespace Localization
{
    public class Language : MonoBehaviour
    {
        private const string English = "en";
        private const string Turkish = "tr";
        private const string Russian = "ru";

        [SerializeField] private string _russianText;
        [SerializeField] private string _englishText;
        [SerializeField] private string _turkishText;

        public string TranslatedText { get; private set; }

        private void Awake()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            Change(languageCode);
        }

        public void Change(string languageCode)
        {
            switch (languageCode)
            {
                case English:
                    TranslatedText = _englishText;
                    break;
                case Turkish:
                    TranslatedText = _turkishText;
                    break;
                case Russian:
                    TranslatedText = _russianText;
                    break;
            }
        }
    }
}