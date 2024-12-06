using Agava.YandexGames;
using UnityEngine;

namespace Localization
{
    public class Language : MonoBehaviour
    {
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
                case Constants.English:
                    TranslatedText = _englishText;
                    break;
                case Constants.Turkish:
                    TranslatedText = _turkishText;
                    break;
                case Constants.Russian:
                    TranslatedText = _russianText;
                    break;
            }
        }
    }
}