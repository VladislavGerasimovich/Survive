using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableItem : Item
{
    [SerializeField] protected TMP_Text _textOfCount;
    [SerializeField] protected int _currentCount;
    [SerializeField] protected int _maxCount;

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

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public override void SetStatus()
    {
        Debug.Log("setstatus");
        _currentCount++;
        SetTextOfCount();

        if(_currentCount == _maxCount)
        {
            _button.interactable = false;
        }
    }

    public void SetTextOfCount()
    {
        _textOfCount.text = $"{_currentCount} / {_maxCount}";
        _text.text = _cost[0];
    }

    public override void OnClick()
    {
        Clicked?.Invoke(_cost[0], _type);
    }

    public override void SetCost()
    {
        _text.text = _cost[0];

        if (_currentCount == _maxCount)
        {
            _button.interactable = false;
        }
    }
}
