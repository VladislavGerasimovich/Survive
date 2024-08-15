using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindowForGame : PopUpWindow
{
    [SerializeField] private ZombiesPools _zombiesPools;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private GrenadesCreator _grenadesCreator;
    [SerializeField] private AudioSource _playerHurt;
    [SerializeField] private List<AudioSource> _woodBatsSound;

    public new bool IsOpen { get; private set; }

    public new event Action YesButtonClicked;
    public new event Action NoButtonClicked;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _yesButton.onClick.AddListener(OnYesButtonClick);
        _noButton.onClick.AddListener(OnNoButtonClick);
    }

    private void OnDisable()
    {
        _yesButton.onClick.RemoveListener(OnYesButtonClick);
        _noButton.onClick.RemoveListener(OnNoButtonClick);
    }

    public override void Open(string message = null)
    {
        _yesButton.interactable = true;
        IsOpen = true;
        _gameTime.Stop();
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _zombiesPools.PauseSound();
        _shotSound.Stop();
        _shotSound.volume = 0;
        _playerHurt.volume = 0;
        _grenadesCreator.PauseSound();

        foreach (AudioSource woodBatSound in _woodBatsSound)
        {
            woodBatSound.Stop();
            woodBatSound.volume = 0;
        }

        if (message != null)
        {
            _text.text = message;
        }
    }

    public override void Close()
    {
        IsOpen = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _gameTime.Run();
        _playerHurt.volume = 1;
        _shotSound.volume = 1;
        _zombiesPools.PlaySound();
        _grenadesCreator.PlaySound();

        foreach (AudioSource woodBatSound in _woodBatsSound)
        {
            woodBatSound.volume = 1;
        }
    }

    public override void OnYesButtonClick()
    {
        _yesButton.interactable = false;
        YesButtonClicked.Invoke();
    }

    public override void OnNoButtonClick()
    {
        Close();
        NoButtonClicked.Invoke();
    }
}
