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
    [SerializeField] private CanvasGroup MenuCanvasGroup;
    [SerializeField] private CanvasGroup TitleCanvasGroup;
    [SerializeField] private CanvasGroup HighScorePanelCanvasGroup;
    [SerializeField] private int _loadSceneId;

    private YandexLeaderboard _yandexLeaderboard;

    private void OnEnable()
    {
        _play.onClick.AddListener(RunGame);
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
        _play.onClick.RemoveListener(RunGame);
        _highScore.onClick.RemoveListener(OpenLiderboard);
        _closeHighScore.onClick.RemoveListener(CloseHighScorePanel);
    }

    private void RunGame()
    {
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
