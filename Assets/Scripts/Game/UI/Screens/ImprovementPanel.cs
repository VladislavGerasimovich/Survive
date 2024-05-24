using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ImprovementPanel : Window
{
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _immortality;
    [SerializeField] private CanvasGroup _gameMenuCanvasGroup;
    [SerializeField] private CanvasGroup _endGameCanvasGroup;
    [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;

    public event Action IsPanelOpen;

    public bool IsOpen { get ; private set; }

    public override void Close()
    {
        _continueGamePanelCanvasGroup.blocksRaycasts = true;
        _gameMenuCanvasGroup.blocksRaycasts = true;
        _endGameCanvasGroup.blocksRaycasts = true;
        CanvasGroup.alpha = 0;
        IsOpen = false;
        _immortality.InteractableOn();

        if (_firstAidButton.Interactable == true)
        {
            _firstAidButton.InteractableOn();
        }
    }

    public override void Open()
    {
        _continueGamePanelCanvasGroup.blocksRaycasts = false;
        _gameMenuCanvasGroup.blocksRaycasts = false;
        _endGameCanvasGroup.blocksRaycasts = false;
        IsOpen = true;
        CanvasGroup.blocksRaycasts = true;
        _firstAidButton.InteractableOff();
        _immortality.InteractableOff();
        CanvasGroup.alpha = 1;
        IsPanelOpen?.Invoke();
    }
}
