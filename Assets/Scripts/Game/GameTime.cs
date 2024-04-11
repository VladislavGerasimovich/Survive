using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] private MobilePlayerInput _mobilePlayerInput;
    [SerializeField] private DesktopPlayerInput _desktopPlayerInput;
    [SerializeField] private ImprovementPanel _improvementPanel;
    [SerializeField] private GameMenuPanel _gameMenuPanel;
    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private ContinueGamePanel _continueGamePanel;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = _desktopPlayerInput;

        if (Application.isMobilePlatform == true)
        {
            _playerInput = _mobilePlayerInput;
        }

        Run();
    }

    public void Stop()
    {
        Time.timeScale = 0;
        _playerInput.DisableInput();
    }

    public void Run()
    {
        if(_improvementPanel.IsOpen == false && _gameMenuPanel.IsOpen == false && _endGamePanel.IsOpen == false && _continueGamePanel.IsOpen == false)
        {
            Time.timeScale = 1;
            _playerInput.EnableInput();
        }
    }
}