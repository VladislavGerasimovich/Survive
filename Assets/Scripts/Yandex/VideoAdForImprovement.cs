using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class VideoAdForImprovement : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;

    private AudioSource _audioSource;

    public event UnityAction RewardReceived;
    public event UnityAction OnCloseAd;

    public bool IsOpen { get; private set; }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    private void OnOpenCallback()
    {
        IsOpen = true;
        Time.timeScale = 0;
        _gameTime.Stop();
        _audioSource.Pause();
    }

    private void OnRewardCallback()
    {
        RewardReceived.Invoke();
    }

    private void OnCloseCallback()
    {
        IsOpen = false;
        OnCloseAd?.Invoke();
        _gameTime.Stop();
        _audioSource.Play();
    }
}
