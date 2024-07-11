using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGamePanel : Window
{
    [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
    [SerializeField] private CanvasGroup _gameMenuPanelCanvasGroup;
    [SerializeField] private CanvasGroup _endGamePanelCanvasGroup;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _immortality;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private PressButton _continueButton;
    [SerializeField] private PressButton _exitButton;
    [SerializeField] private MenuLoader _menuLoader;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Reward _reward;
    [SerializeField] private AudioSource _mainMusic;

    private bool _isRewardReceived;

    private void OnEnable()
    {
        _continueButton.GetComponent<Button>().onClick.AddListener(OnContinueButtonClick);
        _exitButton.GetComponent<Button>().onClick.AddListener(ExitMenu);
    }

    private void OnDisable()
    {
        _continueButton.GetComponent<Button>().onClick.RemoveListener(OnContinueButtonClick);
        _exitButton.GetComponent<Button>().onClick.RemoveListener(ExitMenu);
    }

    public override void Close()
    {
        _videoAd.OnRewardReceived -= OnRewardReceived;
        _videoAd.OnCloseAd -= Close;

        if (_isRewardReceived == true)
        {
            base.Close();

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

            _isRewardReceived = false;
            _menuButton.InteractableOn();
            _immortality.InteractableOn();
            _continueButton.InteractableOff();
            _exitButton.InteractableOff();
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.alpha = 0;
            _gameTime.Run();

            if (_firstAidButton.Interactable == true)
            {
                _firstAidButton.InteractableOn();
            }
        }
    }

    public override void Open()
    {
        base.Open();
        _rewardText.text = _reward.ExtraLevelReward.ToString();
        _improvementPanelCanvasGroup.blocksRaycasts = false;
        _gameMenuPanelCanvasGroup.blocksRaycasts = false;
        _endGamePanelCanvasGroup.blocksRaycasts = false;
        _firstAidButton.InteractableOff();
        _immortality.InteractableOff();
        _menuButton.InteractableOff();
        _continueButton.InteractableOn();
        _exitButton.InteractableOn();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
        _gameTime.Stop();
    }

    private void ExitMenu()
    {
        _mainMusic.Stop();
        StopAllCoroutines();
        _menuLoader.RunInterstitialAd();
    }

    private void OnContinueButtonClick()
    {
        _videoAd.Show();
        _videoAd.OnRewardReceived += OnRewardReceived;
        _videoAd.OnCloseAd += Close;
    }

    private void OnRewardReceived()
    {
        _timer.SetRemainingTime();
        _isRewardReceived = true;
    }
}