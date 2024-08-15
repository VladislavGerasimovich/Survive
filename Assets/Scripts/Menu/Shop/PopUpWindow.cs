using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpWindow : MonoBehaviour
{
    [SerializeField] protected TMP_Text _text;
    [SerializeField] protected Button _yesButton;
    [SerializeField] protected Button _noButton;
    [SerializeField] protected GameTime _gameTime;

    protected CanvasGroup _canvasGroup;

    public bool IsOpen { get; private set; }

    public event Action YesButtonClicked;
    public event Action NoButtonClicked;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open(string message = null)
    {
        _yesButton.interactable = true;
        IsOpen = true;
        _gameTime.Stop();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _yesButton.onClick.AddListener(OnYesButtonClick);
        _noButton.onClick.AddListener(OnNoButtonClick);

        if(message != null)
        {
            _text.text = message;
        }
    }

    public virtual void OnYesButtonClick()
    {
        _yesButton.interactable = false;
        _yesButton.onClick.RemoveListener(OnYesButtonClick);
        _noButton.onClick.RemoveListener(OnNoButtonClick);
        YesButtonClicked.Invoke();
        Close();
    }

    public virtual void OnNoButtonClick()
    {
        _yesButton.onClick.RemoveListener(OnYesButtonClick);
        _noButton.onClick.RemoveListener(OnNoButtonClick);
        NoButtonClicked.Invoke();
        Close();
    }

    public virtual void Close()
    {
        IsOpen = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _gameTime.Run();
    }
}
