using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace YandexElements
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "English";
        private const string TurkishCode = "Turkish";
        private const string RussianCode = "Russian";

        [SerializeField] private LeanLocalization _leanLocalization;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
        }

        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case Constants.English:
                    _leanLocalization.SetCurrentLanguage(EnglishCode);
                    break;
                case Constants.Turkish:
                    _leanLocalization.SetCurrentLanguage(TurkishCode);
                    break;
                case Constants.Russian:
                    _leanLocalization.SetCurrentLanguage(RussianCode);
                    break;
            }
        }
    }
}