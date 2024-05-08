using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    private CanvasGroup _canvasGroup;

    public event Action YesButtonClicked;
    public event Action NoButtonClicked;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open(string message)
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _yesButton.onClick.AddListener(OnYesButtonClick);
        _noButton.onClick.AddListener(OnNoButtonClick);
        _text.text = message;
    }

    private void OnYesButtonClick()
    {
        _yesButton.onClick.RemoveListener(OnYesButtonClick);
        _noButton.onClick.RemoveListener(OnNoButtonClick);
        YesButtonClicked.Invoke();
        Close();
    }

    private void OnNoButtonClick()
    {
        _yesButton.onClick.RemoveListener(OnYesButtonClick);
        _noButton.onClick.RemoveListener(OnNoButtonClick);
        NoButtonClicked.Invoke();
        Close();
    }

    public virtual void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
