using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;
    [SerializeField] protected List<string> _cost;
    [SerializeField] protected string _type;
    [SerializeField] protected PopUpWindow _popUpWindow;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;
    [SerializeField] private string _russianText;

    private const string English = "en";
    private const string Turkish = "tr";
    private const string Russian = "ru";

    protected Image _background;
    protected Button _button;
    protected int _indexOfCost;
    protected List<Color> _colors;
    private string _translatedText;

    public virtual event Action<string, string> Clicked;

    public string Type { get; protected set; }

    private void Awake()
    {
        _colors = new List<Color>
        {
            new Color32(125, 120, 126, 255),
            new Color32(170, 238, 147, 255),
            new Color32(54, 189, 240, 255),
            new Color32(244, 105, 255, 255),
            new Color32(229, 214, 75, 255)
        };

        _background = GetComponent<Image>();
        _button = GetComponent<Button>();
        Type = _type;
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        ChangeLanguage(languageCode);
    }

    private void Start()
    {
        SetCost();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case English:
                _translatedText = _englishText;
                break;
            case Turkish:
                _translatedText = _turkishText;
                break;
            case Russian:
                _translatedText = _russianText;
                break;
        }
    }

    public virtual void SetCost()
    {
        _text.text = _cost[_indexOfCost];
        _background.color = _colors[_indexOfCost];
    }

    public virtual void SetStatus()
    {
        if(_indexOfCost < _cost.Count)
        {
            _indexOfCost++;

            if (_indexOfCost != _cost.Count)
            {
                SetCost();
            }

            if (_indexOfCost == _cost.Count - 1)
            {
                _button.interactable = false;
            }
        }
    }

    public void OnClick()
    {
        _popUpWindow.YesButtonClicked += Buy;
        _popUpWindow.NoButtonClicked += Cancel;
        _popUpWindow.Open(_translatedText);
    }

    public virtual void Buy()
    {
        _popUpWindow.YesButtonClicked -= Buy;
        _popUpWindow.NoButtonClicked -= Cancel;
        Clicked?.Invoke(_cost[_indexOfCost], _type);
    }

    public void Cancel()
    {
        _popUpWindow.YesButtonClicked -= Buy;
        _popUpWindow.NoButtonClicked -= Cancel;
    }
}