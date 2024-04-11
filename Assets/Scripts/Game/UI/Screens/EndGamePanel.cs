using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : Window
{
    [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
    [SerializeField] private CanvasGroup _gameMenuPanelCanvasGroup;
    [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _reliveButton;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private MenuLoader _menuLoader;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private VideoAd _videoAd;

    private bool _canUse;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        _canUse = true;
    }

    private void OnEnable()
    {
        _videoAd.OnCloseAd += Close;
        _reliveButton.GetComponent<Button>().onClick.AddListener(OnReliveButtonClick);
        _menuButton.GetComponent<Button>().onClick.AddListener(ExitMenu);
    }

    private void OnDisable()
    {
        _videoAd.OnCloseAd -= Close;
        _reliveButton.GetComponent<Button>().onClick.RemoveListener(OnReliveButtonClick);
        _menuButton.GetComponent<Button>().onClick.RemoveListener(ExitMenu);
    }

    public override void Close()
    {
        if (_improvementPanelCanvasGroup.alpha == 1)
        {
            _improvementPanelCanvasGroup.blocksRaycasts = true;
        }

        if (_gameMenuPanelCanvasGroup.alpha == 1)
        {
            _gameMenuPanelCanvasGroup.blocksRaycasts = true;
        }

        if (_continueGamePanelCanvasGroup.alpha == 1)
        {
            _continueGamePanelCanvasGroup.blocksRaycasts = true;
        }

        CanvasGroup.blocksRaycasts = false;
        IsOpen = false;
        _reliveButton.InteractableOff();
        _menuButton.InteractableOff();
        CanvasGroup.alpha = 0;
        _gameTime.Run();

        if (_firstAidButton.Interactable == true)
        {
            _firstAidButton.InteractableOn();
        }
    }

    public override void Open()
    {
        if (_canUse == true)
        {
            _continueGamePanelCanvasGroup.blocksRaycasts = false;
            _improvementPanelCanvasGroup.blocksRaycasts = false;
            _gameMenuPanelCanvasGroup.blocksRaycasts = false;
            IsOpen = true;
            _firstAidButton.InteractableOff();
            _menuButton.InteractableOn();
            _reliveButton.InteractableOn();
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.alpha = 1;
            _gameTime.Stop();
        
            return;
        }
        else
        {
            ExitMenu();
        }
    }

    private void OnReliveButtonClick()
    {
        _videoAd.Show();
        _canUse = false;
        _firstAidButton.StatusInteractableOff();
    }

    private void ExitMenu()
    {
        Close();
        StopAllCoroutines();
        _menuLoader.RunMenu();
    }
}
