using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuPanel : Window
{
    [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
    [SerializeField] private CanvasGroup _endGamePanelCanvasGroup;
    [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;
    [SerializeField] private Button _gameMenuButton;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private PressButton _returnToGameButton;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _immortalityButton;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private MenuLoader _menuLoader;
    [SerializeField] private TimeOfAction _timeOfAction;
    [SerializeField] private PlayerMortality _playerMortality;

    private void OnEnable()
    {
        _gameMenuButton.onClick.AddListener(Open);
        _returnToGameButton.GetComponent<Button>().onClick.AddListener(Close);
        _menuButton.GetComponent<Button>().onClick.AddListener(ExitMenu);
    }

    private void OnDisable()
    {
        _gameMenuButton.onClick.RemoveListener(Open);
        _returnToGameButton.GetComponent<Button>().onClick.RemoveListener(Close);
        _menuButton.GetComponent<Button>().onClick.RemoveListener(ExitMenu);
    }

    public override void Close()
    {
        base.Close();

        if (_improvementPanelCanvasGroup.alpha == 1)
        {
            _improvementPanelCanvasGroup.blocksRaycasts = true;
        }

        if (_endGamePanelCanvasGroup.alpha == 1)
        {
            _endGamePanelCanvasGroup.blocksRaycasts = true;
        }

        if (_continueGamePanelCanvasGroup.alpha == 1)
        {
            _continueGamePanelCanvasGroup.blocksRaycasts = true;
        }

        _timeOfAction.AllowUse();
        _playerMortality.AllowUse();
        _gameMenuButton.interactable = true;
        _immortalityButton.Enable();
        _immortalityButton.InteractableOn();
        _returnToGameButton.InteractableOff();
        _menuButton.InteractableOff();
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0;
        _gameTime.Run();
        _firstAidButton.Enable();

        if (_firstAidButton.Interactable == true)
        {
            _firstAidButton.InteractableOn();
        }
    }

    public override void Open()
    {
        base.Open();
        _timeOfAction.ProhibitUse();
        _playerMortality.ProhibitUse();
        _gameMenuButton.interactable = false;
        _improvementPanelCanvasGroup.blocksRaycasts = false;
        _endGamePanelCanvasGroup.blocksRaycasts = false;
        _continueGamePanelCanvasGroup.blocksRaycasts = false;
        _firstAidButton.Disable();
        _firstAidButton.InteractableOff();
        _immortalityButton.Disable();
        _immortalityButton.InteractableOff();
        _returnToGameButton.InteractableOn();
        _menuButton.InteractableOn();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
        _gameTime.Stop();
    }

    private void ExitMenu()
    {
        StopAllCoroutines();
        _menuLoader.RunInterstitialAd();
    }
}