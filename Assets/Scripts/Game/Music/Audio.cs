using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    [SerializeField] private List<Window> _windows;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private InterstitialAd _interstitialAd;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        foreach (var window in _windows)
        {
            window.IsPanelOpen += Pause;
            window.IsPanelClose += PlayAfterPause;
        }

        _videoAd.OnOpenAd += Pause;
        _interstitialAd.OnOpenAd += Stop;
        _videoAd.OnCloseAd += PlayAfterPause;
    }

    private void OnDisable()
    {
        foreach (var window in _windows)
        {
            window.IsPanelOpen -= Pause;
            window.IsPanelClose -= PlayAfterPause;
        }

        _videoAd.OnOpenAd -= Pause;
        _interstitialAd.OnOpenAd -= Stop;
        _videoAd.OnCloseAd -= PlayAfterPause;
    }

    public void Pause()
    {
        _audioSource.Pause();
        _audioSource.volume = 0;
    }

    public void Play()
    {
        foreach (var window in _windows)
        {
            if (window.IsOpen == true)
            {
                return;
            }
        }

        _audioSource.Play();
        _audioSource.volume = 1;
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void PlayAfterPause()
    {
        if(_audioSource.time > 0)
        {
            _audioSource.Play();
        }
    }
}
