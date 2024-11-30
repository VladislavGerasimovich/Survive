using System.Collections.Generic;
using Agava.YandexGames;
using Game.Buttons;
using Menu.DifficultyLevels;
using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YandexElements;

namespace TypedScenes
{
    [RequireComponent(typeof(PlayerDataManager))]
    [RequireComponent(typeof(YandexLeaderboard))]
    [RequireComponent(typeof(YandexElements.InterstitialAd))]
    [RequireComponent(typeof(InterstitialAdTimer))]
    public class LoadSceneLoader : MonoBehaviour
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _highScore;
        [SerializeField] private Button _closeHighScore;
        [SerializeField] private Button _shop;
        [SerializeField] private Button _closeShop;
        [SerializeField] private CanvasGroup MenuCanvasGroup;
        [SerializeField] private CanvasGroup TitleCanvasGroup;
        [SerializeField] private CanvasGroup HighScorePanelCanvasGroup;
        [SerializeField] private CanvasGroup DifficultyLevelsPanelCanvasGroup;
        [SerializeField] private CanvasGroup ShopCanvasGroup;
        [SerializeField] private CanvasGroup _popupCanvasGroup;
        [SerializeField] private PressButton _popupYesButton;
        [SerializeField] private PressButton _popupNoButton;
        [SerializeField] private List<DifficultyCard> _difficultyCards;
        [SerializeField] private int _loadSceneId;
        [SerializeField] private int _menuSceneId;

        private PlayerDataManager _playerDataManager;
        private YandexLeaderboard _yandexLeaderboard;
        private YandexElements.InterstitialAd _interstitialAd;
        private InterstitialAdTimer _interstitialAdTimer;
        private bool _isFillLeaderboard;
        private bool _isFillShop;
        private int _numberOfAdImpressions;

        private void OnEnable()
        {
            _shop.onClick.AddListener(OnShopButtonClick);
            _closeShop.onClick.AddListener(OnCloseShopButtonClick);
            _play.onClick.AddListener(OnPlayButtonClick);
            _highScore.onClick.AddListener(OpenLiderboard);
            _closeHighScore.onClick.AddListener(CloseHighScorePanel);
            _yandexLeaderboard = GetComponent<YandexLeaderboard>();
            int playerScore = PlayerPrefs.GetInt("PlayerScore");
            _yandexLeaderboard.SetPlayerScore(playerScore);
        }

        private void Awake()
        {
            _interstitialAd = GetComponent<YandexElements.InterstitialAd>();
            _interstitialAdTimer = GetComponent<InterstitialAdTimer>();
            _playerDataManager = GetComponent<PlayerDataManager>();
            _numberOfAdImpressions = PlayerPrefs.GetInt("NumberOfAdImpressions", 0);
            Time.timeScale = 1;
        }

        private void OnDisable()
        {
            _shop.onClick.RemoveListener(OnShopButtonClick);
            _closeShop.onClick.RemoveListener(OnCloseShopButtonClick);
            _play.onClick.RemoveListener(OnPlayButtonClick);
            _highScore.onClick.RemoveListener(OpenLiderboard);
            _closeHighScore.onClick.RemoveListener(CloseHighScorePanel);
        }

        private void OnShopButtonClick()
        {
            if (PlayerAccount.IsAuthorized == true)
            {
                if (_isFillShop == false)
                {
                    _playerDataManager.GetCloudSaveData();
                    _isFillShop = true;
                }

                DifficultyLevelsPanelCanvasGroup.alpha = 0;
                DifficultyLevelsPanelCanvasGroup.blocksRaycasts = false;
                MenuCanvasGroup.alpha = 0;
                MenuCanvasGroup.blocksRaycasts = false;
                TitleCanvasGroup.alpha = 0;
                TitleCanvasGroup.blocksRaycasts = false;
                HighScorePanelCanvasGroup.alpha = 0;
                HighScorePanelCanvasGroup.blocksRaycasts = false;
                ShopCanvasGroup.alpha = 1;
                ShopCanvasGroup.blocksRaycasts = true;
            }

            if (PlayerAccount.IsAuthorized == false)
            {
                _popupYesButton.Click += Authorize;
                _popupNoButton.Click += Cancel;
                _popupCanvasGroup.alpha = 1;
                _popupCanvasGroup.blocksRaycasts = true;
                MenuCanvasGroup.alpha = 0;
                MenuCanvasGroup.blocksRaycasts = false;
                TitleCanvasGroup.alpha = 0;
                TitleCanvasGroup.blocksRaycasts = false;
                return;
            }
        }

        private void OnCloseShopButtonClick()
        {
            ShopCanvasGroup.alpha = 0;
            ShopCanvasGroup.blocksRaycasts = false;
            MenuCanvasGroup.alpha = 1;
            MenuCanvasGroup.blocksRaycasts = true;
            TitleCanvasGroup.alpha = 1;
            TitleCanvasGroup.blocksRaycasts = true;
        }

        private void OnPlayButtonClick()
        {
            DifficultyLevelsPanelCanvasGroup.alpha = 1;
            DifficultyLevelsPanelCanvasGroup.blocksRaycasts = true;
            MenuCanvasGroup.alpha = 0;
            MenuCanvasGroup.blocksRaycasts = false;
            TitleCanvasGroup.alpha = 0;
            TitleCanvasGroup.blocksRaycasts = false;
            HighScorePanelCanvasGroup.alpha = 0;
            HighScorePanelCanvasGroup.blocksRaycasts = false;

            foreach (DifficultyCard difficultyCard in _difficultyCards)
            {
                difficultyCard.Click += OnDifficultyCardClick;
            }
        }

        private void OnDifficultyCardClick()
        {
            Debug.Log(_interstitialAdTimer.IsReached + " interstitialAdIsReached");

            if (_interstitialAdTimer.IsReached == false && _numberOfAdImpressions > 0)
            {
                LoadScene();

                foreach (DifficultyCard difficultyCard in _difficultyCards)
                {
                    difficultyCard.Click -= OnDifficultyCardClick;
                }

                return;
            }

            _numberOfAdImpressions++;
            PlayerPrefs.SetInt("NumberOfAdImpressions", _numberOfAdImpressions);
            PlayerPrefs.Save();
            _interstitialAd.Show();
            _interstitialAd.OnCloseAd += LoadScene;

            foreach (DifficultyCard difficultyCard in _difficultyCards)
            {
                difficultyCard.Click -= OnDifficultyCardClick;
            }
        }

        private void LoadScene()
        {
            _interstitialAd.OnCloseAd -= LoadScene;
            SceneManager.LoadScene(_loadSceneId);
        }

        private void OpenLiderboard()
        {
            if (PlayerAccount.IsAuthorized == true)
            {
                if (_isFillLeaderboard == false)
                {
                    _yandexLeaderboard.Fill();
                    _isFillLeaderboard = true;
                }

                PlayerAccount.RequestPersonalProfileDataPermission();
                OpenHighScorePanel();
            }

            if (PlayerAccount.IsAuthorized == false)
            {
                _popupYesButton.Click += Authorize;
                _popupNoButton.Click += Cancel;
                _popupCanvasGroup.alpha = 1;
                _popupCanvasGroup.blocksRaycasts = true;
                MenuCanvasGroup.alpha = 0;
                MenuCanvasGroup.blocksRaycasts = false;
                TitleCanvasGroup.alpha = 0;
                TitleCanvasGroup.blocksRaycasts = false;
                return;
            }
        }

        private void Authorize()
        {
            PlayerAccount.Authorize();
            _popupCanvasGroup.alpha = 0;
            _popupCanvasGroup.blocksRaycasts = false;
            MenuCanvasGroup.alpha = 1;
            MenuCanvasGroup.blocksRaycasts = true;
            TitleCanvasGroup.alpha = 1;
            TitleCanvasGroup.blocksRaycasts = true;
            _popupYesButton.Click -= Authorize;
            _popupNoButton.Click -= Cancel;
        }

        private void Cancel()
        {
            _popupCanvasGroup.alpha = 0;
            _popupCanvasGroup.blocksRaycasts = false;
            MenuCanvasGroup.alpha = 1;
            MenuCanvasGroup.blocksRaycasts = true;
            TitleCanvasGroup.alpha = 1;
            TitleCanvasGroup.blocksRaycasts = true;
            _popupYesButton.Click -= Authorize;
            _popupNoButton.Click -= Cancel;
        }

        private void OpenHighScorePanel()
        {
            DifficultyLevelsPanelCanvasGroup.alpha = 0;
            DifficultyLevelsPanelCanvasGroup.blocksRaycasts = false;
            MenuCanvasGroup.alpha = 0;
            MenuCanvasGroup.blocksRaycasts = false;
            TitleCanvasGroup.alpha = 0;
            TitleCanvasGroup.blocksRaycasts = false;
            HighScorePanelCanvasGroup.alpha = 1;
            HighScorePanelCanvasGroup.blocksRaycasts = true;
        }

        private void CloseHighScorePanel()
        {
            MenuCanvasGroup.alpha = 1;
            MenuCanvasGroup.blocksRaycasts = true;
            TitleCanvasGroup.alpha = 1;
            TitleCanvasGroup.blocksRaycasts = true;
            HighScorePanelCanvasGroup.alpha = 0;
            HighScorePanelCanvasGroup.blocksRaycasts = false;
        }
    }
}