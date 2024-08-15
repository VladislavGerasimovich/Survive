using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UIElements;

public class TestFocus : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void Start()
    {
        if(_audioSource != null && _audioSource.enabled == true)
        {
            _audioSource.Play();
        }
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackgroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if(value == true)
        {
            AudioListener.volume = 0;
            return;
        }

        if (_audioSource != null && _audioSource.enabled == true)
        {
            _audioSource.volume = value ? 0 : 1;
        }

        AudioListener.volume = 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
