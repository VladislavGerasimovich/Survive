using Agava.YandexGames;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumableItem : Item
{
    [SerializeField] protected TMP_Text TextOfCount;
    [SerializeField] protected int CurrentCount;
    [SerializeField] protected int MaxCount;

    public override event Action<string, string> Clicked;

    private void Awake()
    {
        Background = GetComponent<Image>();
        Button = GetComponent<Button>();
        Class = Type;
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        ChangeLanguage(languageCode);
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnClick);
    }

    public override void SetStatus()
    {
        Debug.Log("setstatus");
        CurrentCount++;
        SetTextOfCount();

        if(CurrentCount == MaxCount)
        {
            Button.interactable = false;
        }
    }

    public void SetTextOfCount()
    {
        TextOfCount.text = $"{CurrentCount} / {MaxCount}";
        Text.text = Cost[0];
    }

    public override void OnClick()
    {
        Clicked?.Invoke(Cost[0], Type);
    }

    public override void SetCost()
    {
        Text.text = Cost[0];

        if (CurrentCount == MaxCount)
        {
            Button.interactable = false;
        }
    }
}
