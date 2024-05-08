using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(YandexLeaderboard))]
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
    [SerializeField] private DifficultyCard[] _difficultyCards;
    [SerializeField] private int _loadSceneId;

    private YandexLeaderboard _yandexLeaderboard;

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
        _yandexLeaderboard.Fill();
    }

    private void Awake()
    {
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
        foreach (DifficultyCard difficultyCard in _difficultyCards)
        {
            difficultyCard.Click -= OnDifficultyCardClick;
        }

        SceneManager.LoadScene(_loadSceneId);
    }

    private void OpenLiderboard()
    {
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == true)
        {
            //PlayerAccount.RequestPersonalProfileDataPermission();
            OpenHighScorePanel();
        }

        if(PlayerAccount.IsAuthorized == false)
        {
            return;
        }
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
