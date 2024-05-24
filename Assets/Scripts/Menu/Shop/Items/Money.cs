using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : Item
{
    [SerializeField] private VideoAd _videoAd;

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

    public override void Buy()
    {
        _popUpWindow.YesButtonClicked -= Buy;
        _popUpWindow.NoButtonClicked -= Cancel;
        _videoAd.Show();
        _videoAd.OnCloseAd += AddReward;
    }

    private void AddReward()
    {
        Clicked?.Invoke(_cost[0], _type);
    }
}
