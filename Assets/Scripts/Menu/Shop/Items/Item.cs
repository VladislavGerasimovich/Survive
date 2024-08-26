using Agava.YandexGames;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;
    [SerializeField] protected List<string> Cost;
    [SerializeField] protected string Type;
    [SerializeField] protected PopUpWindow PopUpWindow;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;
    [SerializeField] private string _russianText;

    private const string English = "en";
    private const string Turkish = "tr";
    private const string Russian = "ru";

    protected Image Background;
    protected Button Button;
    protected int IndexOfCost;
    protected List<Color> Colors;

    public virtual event Action<string, string> Clicked;

    public string TranslatedText { get; private set; }
    public string Class { get; protected set; }

    private void Awake()
    {
        Colors = new List<Color>
        {
            new Color32(125, 120, 126, 255),
            new Color32(170, 238, 147, 255),
            new Color32(54, 189, 240, 255),
            new Color32(244, 105, 255, 255),
            new Color32(229, 214, 75, 255)
        };

        Background = GetComponent<Image>();
        Button = GetComponent<Button>();
        Class = Type;
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        ChangeLanguage(languageCode);
    }

    private void Start()
    {
        SetCost();
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnClick);
    }

    public void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case English:
                TranslatedText = _englishText;
                break;
            case Turkish:
                TranslatedText = _turkishText;
                break;
            case Russian:
                TranslatedText = _russianText;
                break;
        }
    }

    public virtual void SetCost()
    {
        Text.text = Cost[IndexOfCost];
        Background.color = Colors[IndexOfCost];

        if (IndexOfCost == Cost.Count - 1)
        {
            Button.interactable = false;
        }
    }

    public virtual void SetStatus()
    {
        if(IndexOfCost < Cost.Count)
        {
            IndexOfCost++;

            if (IndexOfCost != Cost.Count)
            {
                SetCost();
            }

            if (IndexOfCost == Cost.Count - 1)
            {
                Button.interactable = false;
            }
        }
    }

    public virtual void OnClick()
    {
        Clicked?.Invoke(Cost[IndexOfCost], Type);
    }
}