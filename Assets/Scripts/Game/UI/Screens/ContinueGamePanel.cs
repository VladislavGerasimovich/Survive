using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGamePanel : Window
{
    [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
    [SerializeField] private CanvasGroup _gameMenuPanelCanvasGroup;
    [SerializeField] private CanvasGroup _endGamePanelCanvasGroup;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private PressButton _continueButton;
    [SerializeField] private PressButton _exitButton;
    [SerializeField] private MenuLoader _menuLoader;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private VideoAd _videoAd;

    public bool IsOpen { get; private set; }

    private void OnEnable()
    {
        _continueButton.GetComponent<Button>().onClick.AddListener(Close);
        _exitButton.GetComponent<Button>().onClick.AddListener(ExitMenu);
    }

    private void OnDisable()
    {
        _continueButton.GetComponent<Button>().onClick.RemoveListener(Close);
        _exitButton.GetComponent<Button>().onClick.RemoveListener(ExitMenu);
    }

    public override void Close()
    {
        _videoAd.Show();
        IsOpen = false;

        if (_improvementPanelCanvasGroup.alpha == 1)
        {
            _improvementPanelCanvasGroup.blocksRaycasts = true;
        }

        if (_gameMenuPanelCanvasGroup.alpha == 1)
        {
            _gameMenuPanelCanvasGroup.blocksRaycasts = true;
        }

        if (_endGamePanelCanvasGroup.alpha == 1)
        {
            _endGamePanelCanvasGroup.blocksRaycasts = true;
        }

        _menuButton.InteractableOn();
        _continueButton.InteractableOff();
        _exitButton.InteractableOff();
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.alpha = 0;

        if (_firstAidButton.Interactable == true)
        {
            _firstAidButton.InteractableOn();
        }
    }

    public override void Open()
    {
        IsOpen = true;
        _improvementPanelCanvasGroup.blocksRaycasts = false;
        _gameMenuPanelCanvasGroup.blocksRaycasts = false;
        _endGamePanelCanvasGroup.blocksRaycasts = false;
        _firstAidButton.InteractableOff();
        _menuButton.InteractableOff();
        _continueButton.InteractableOn();
        _exitButton.InteractableOn();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
        _gameTime.Stop();
    }

    private void ExitMenu()
    {
        Close();
        StopAllCoroutines();
        _menuLoader.RunMenu();
    }
}
