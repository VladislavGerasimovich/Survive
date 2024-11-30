using Game;
using Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using YandexElements;

namespace TypedScenes
{
    [RequireComponent(typeof(InterstitialAd))]
    [RequireComponent(typeof(InterstitialAdTimer))]
    public class MenuLoader : MonoBehaviour
    {
        [SerializeField] private int _menuSceneId;
        [SerializeField] private PlayerScore _playerScore;
        [SerializeField] private GameTime _gameTime;

        private InterstitialAd _interstitialAd;
        private InterstitialAdTimer _interstitialAdTimer;

        private void Awake()
        {
            _interstitialAd = GetComponent<InterstitialAd>();
            _interstitialAdTimer = GetComponent<InterstitialAdTimer>();
        }

        public void RunInterstitialAd()
        {
            if (_interstitialAdTimer.IsReached == false)
            {
                RunMenu();

                return;
            }

            _interstitialAd.Show();
            _interstitialAd.OnCloseAd += RunMenu;
        }

        private void RunMenu()
        {
            _interstitialAd.OnCloseAd -= RunMenu;
            StopAllCoroutines();

            if (_playerScore != null)
            {
                PlayerPrefs.SetInt("PlayerScore", _playerScore.Score);
                PlayerPrefs.Save();
            }

            SceneManager.LoadScene(_menuSceneId);
        }
    }
}