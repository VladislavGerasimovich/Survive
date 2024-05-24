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

    private const string MONEY = "MONEY";

    private void OnEnable()
    {
        _exitMenuButton.Click += Close;
    }

    private void OnDisable()
    {
        _exitMenuButton.Click -= Close;
    }

    public override void Close()
    {
        _menuLoader.RunMenu();
        _gameTime.Run();
    }

    public override void Open()
    {
        _continueGamePanelCanvasGroup.blocksRaycasts = false;
        _improvementPanelCanvasGroup.blocksRaycasts = false;
        _gameMenuPanelCanvasGroup.blocksRaycasts = false;
        _endGamePanelCanvasGroup.blocksRaycasts = false;
        _exitMenuButton.InteractableOn();
        _immortality.InteractableOn();
        _firstAidButton.InteractableOff();
        _menuButton.InteractableOff();
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 1;
        _gameTime.Stop();
        _rewardText.text = _reward.AllRewards.ToString();
        int money = PlayerPrefs.GetInt(MONEY) + _reward.AllRewards;
        PlayerPrefs.SetInt(MONEY, money);
        PlayerPrefs.Save();
    }
}