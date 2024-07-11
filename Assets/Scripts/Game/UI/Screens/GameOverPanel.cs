using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameOverPanel : Window
{
    [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
    [SerializeField] private CanvasGroup _gameMenuPanelCanvasGroup;
    [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;
    [SerializeField] private CanvasGroup _endGamePanelCanvasGroup;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private PressButton _exitMenuButton;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _immortality;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private MenuLoader _menuLoader;
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private Reward _reward;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private ZombiesPools _zombiesPools;
    [SerializeField] private List<AudioSource> _melleWeaponAudioSources;

    private const string MONEY = "MONEY";

    private void OnEnable()
    {
        _exitMenuButton.Click += OnExitMenuButtonClick;
    }

    private void OnDisable()
    {
        _exitMenuButton.Click -= OnExitMenuButtonClick;
    }

    public override void Open()
    {
        base.Open();
        _continueGamePanelCanvasGroup.blocksRaycasts = false;
        _improvementPanelCanvasGroup.blocksRaycasts = false;
        _gameMenuPanelCanvasGroup.blocksRaycasts = false;
        _endGamePanelCanvasGroup.blocksRaycasts = false;
        _exitMenuButton.InteractableOn();
        _immortality.InteractableOff();
        _firstAidButton.InteractableOff();
        _menuButton.InteractableOff();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
        _rewardText.text = _reward.AllRewards.ToString();

        foreach (AudioSource audio in _melleWeaponAudioSources)
        {
            audio.Stop();
        }

        _zombiesPools.StopSound();
        _shotSound.Stop();
        _gameTime.Stop();
        int money = PlayerPrefs.GetInt(MONEY) + _reward.AllRewards;
        PlayerPrefs.SetInt(MONEY, money);
        PlayerPrefs.Save();
    }

    private void OnExitMenuButtonClick()
    {
        _menuLoader.RunInterstitialAd();
    }
}