using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : Item
{
    [SerializeField] private VideoAd _videoAd;

    private bool _isRewardReceived;

    public override event Action<string, string> Clicked;

    private void Awake()
    {
        _background = GetComponent<Image>();
        _button = GetComponent<Button>();
        Type = _type;
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        ChangeLanguage(languageCode);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void Start() { }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public override void OnClick()
    {
        _popUpWindow.YesButtonClicked += Buy;
        _popUpWindow.NoButtonClicked += Cancel;
        _popUpWindow.Open(TranslatedText);
    }

    public void Buy()
    {
        _popUpWindow.YesButtonClicked -= Buy;
        _popUpWindow.NoButtonClicked -= Cancel;
        _videoAd.Show();
        _videoAd.OnRewardReceived += PrepareReward;
        _videoAd.OnCloseAd += AddReward;
    }

    private void Cancel()
    {
        _popUpWindow.YesButtonClicked -= Buy;
        _popUpWindow.NoButtonClicked -= Cancel;
        _popUpWindow.Close();
    }

    private void AddReward()
    {
        _videoAd.OnRewardReceived -= PrepareReward;
        _videoAd.OnCloseAd -= AddReward;

        if(_isRewardReceived == true)
        {
            _isRewardReceived = false;
            Clicked?.Invoke(_cost[0], _type);
        }
    }

    private void PrepareReward()
    {
        _isRewardReceived = true;
    }
}
