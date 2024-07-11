using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class VideoAd : MonoBehaviour
{
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private GameTime _gameTime;

    private AudioSource _mainMusic;

    public event UnityAction OnOpenAd;
    public event UnityAction OnRewardReceived;
    public event UnityAction OnCloseAd;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        _mainMusic = GetComponent<AudioSource>();
    }

    public void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    public void OnOpenCallback()
    {
        Time.timeScale = 0;
        IsOpen = true;
        _mainMusic.Pause();
        _shotSound.Stop();
        OnOpenAd.Invoke();
    }

    public void OnRewardCallback()
    {
        OnRewardReceived.Invoke();
    }

    public void OnCloseCallback()
    {
        IsOpen = false;
        OnCloseAd.Invoke();
        _gameTime.Run();
        _mainMusic.Play();

        if(_shotSound.time != 0)
        {
            _shotSound.Play();
        }
    }
}
