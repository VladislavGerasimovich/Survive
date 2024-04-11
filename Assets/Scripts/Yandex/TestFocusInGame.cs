using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UIElements;

public class TestFocusInGame : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private ImprovementPanel _improvementPanel;
    [SerializeField] private GameMenuPanel _gameMenuPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private ContinueGamePanel _continueGamePanel;

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
        _audioSource.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        if (_improvementPanel.IsOpen == false && _gameMenuPanel.IsOpen == false && _endGamePanel.IsOpen == false && _continueGamePanel.IsOpen == false)
        {
            Debug.Log(_improvementPanel.IsOpen + " _improvementPanel!!!");
            Debug.Log(_gameMenuPanel.IsOpen + " _gameMenuPanel!!!");
            Debug.Log(_endGamePanel.IsOpen + " _endGamePanel!!!");
            Debug.Log(_continueGamePanel.IsOpen + " _continueGamePanel!!!");
            _gameTime.Run();
            return;
        }

        _gameTime.Stop();
    }
}
