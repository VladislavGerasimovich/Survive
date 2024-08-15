using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UIElements;

[RequireComponent(typeof(VideoAd))]
public class TestFocusInGame : MonoBehaviour
{
    [SerializeField] private AudioSource _mainMusic;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private AudioSource _playerHurt;
    [SerializeField] private List<Audio> _audio;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private ImprovementPanel _improvementPanel;
    [SerializeField] private GameMenuPanel _gameMenuPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private ContinueGamePanel _continueGamePanel;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private ZombiesPools _zombiesPools;
    [SerializeField] private TrainingPanel _trainingPanel;
    [SerializeField] private VideoAdForImprovement _videoAdForChoosingTwoCards;
    [SerializeField] private VideoAdForImprovement _videoAdForShuffleCards;
    [SerializeField] private PopUpWindowForGame _immortalityPopUpWindowForGame;
    [SerializeField] private PopUpWindowForGame _firstAidPopUpWindowForGame;
    [SerializeField] private AllGameMusic _allGameMusic;

    private VideoAd _videoAd;

    private void Awake()
    {
        _videoAd = GetComponent<VideoAd>();
    }

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if (value == true)
        {
            AudioListener.volume = 0;
            _mainMusic.Pause();
        }

        if (_improvementPanel.IsOpen == false && _gameMenuPanel.IsOpen == false && _endGamePanel.IsOpen == false && _continueGamePanel.IsOpen == false && _gameOverPanel.IsOpen == false && _videoAd.IsOpen == false && _videoAdForChoosingTwoCards.IsOpen == false && _videoAdForShuffleCards.IsOpen == false && _trainingPanel.IsOpen == false && value == false && _immortalityPopUpWindowForGame.IsOpen == false && _firstAidPopUpWindowForGame.IsOpen == false)
        {
            if(_allGameMusic.CanPlay == true)
            {
                foreach (Audio audio in _audio)
                {
                    audio.PlayAfterPause();
                }

                AudioListener.volume = 1;
                _zombiesPools.PlaySound();
                _mainMusic.Play();

                if(_shotSound.time != 0)
                {
                    _shotSound.Play();
                }
            }

            return;
        }
        else
        {
            Debug.Log("Test focus in game ElSE");
            Debug.Log(_allGameMusic.CanPlay + "allgame music can play");
            foreach (Audio audio in _audio)
            {
                audio.Stop();
            }

            _playerHurt.Stop();
            _zombiesPools.PauseSound();
            _shotSound.Pause();
            _mainMusic.Pause();

            Debug.Log("value is " + value);
            Debug.Log("_videoAd.IsOpen is " + _videoAd.IsOpen);
            Debug.Log("_videoAdForChoosingTwoCards.IsOpen is " + _videoAdForChoosingTwoCards.IsOpen);
            Debug.Log("_videoAdForShuffleCards.IsOpen is " + _videoAdForChoosingTwoCards.IsOpen);

            if (value == false && _videoAd.IsOpen == false && _videoAdForChoosingTwoCards.IsOpen == false && _videoAdForShuffleCards.IsOpen == false)
            {
                if (_allGameMusic.CanPlay == true)
                {
                    AudioListener.volume = 1;
                    _mainMusic.Play();
                }
            }
        }
    }

    private void PauseGame(bool value)
    {
        if (_improvementPanel.IsOpen == false && _gameMenuPanel.IsOpen == false && _endGamePanel.IsOpen == false && _continueGamePanel.IsOpen == false && _gameOverPanel.IsOpen == false && _videoAd.IsOpen == false && _videoAdForChoosingTwoCards.IsOpen == false && _videoAdForShuffleCards.IsOpen == false && _trainingPanel.IsOpen == false && value == false && _immortalityPopUpWindowForGame.IsOpen == false && _firstAidPopUpWindowForGame.IsOpen == false)
        {
            _gameTime.Run();
            return;
        }

        Debug.Log(" Stop in TestFocusInGame");
        _gameTime.Stop();
    }
}
