using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpWindow : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;
    [SerializeField] protected Button YesButton;
    [SerializeField] protected Button NoButton;
    [SerializeField] protected GameTime GameTime;

    protected CanvasGroup CanvasGroup;

    public bool IsOpen { get; private set; }

    public event Action YesButtonClicked;
    public event Action NoButtonClicked;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Open(string message = null)
    {
        YesButton.interactable = true;
        IsOpen = true;
        GameTime.Stop();
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        YesButton.onClick.AddListener(OnYesButtonClick);
        NoButton.onClick.AddListener(OnNoButtonClick);

        if(message != null)
        {
            Text.text = message;
        }
    }

    public virtual void OnYesButtonClick()
    {
        YesButton.interactable = false;
        YesButton.onClick.RemoveListener(OnYesButtonClick);
        NoButton.onClick.RemoveListener(OnNoButtonClick);
        YesButtonClicked.Invoke();
        Close();
    }

    public virtual void OnNoButtonClick()
    {
        YesButton.onClick.RemoveListener(OnYesButtonClick);
        NoButton.onClick.RemoveListener(OnNoButtonClick);
        NoButtonClicked.Invoke();
        Close();
    }

    public virtual void Close()
    {
        IsOpen = false;
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        GameTime.Run();
    }
}
