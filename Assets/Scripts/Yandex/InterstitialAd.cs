using System;
using UnityEngine;

namespace YandexElements
{
    [RequireComponent(typeof(AudioSource))]
    public class InterstitialAd : MonoBehaviour
    {
        private AudioSource _mainMusic;

        public event Action OnCloseAd;
        public event Action OnOpenAd;
        public event Action OnErrorAd;

        private void Awake()
        {
            _mainMusic = GetComponent<AudioSource>();
        }

        public void Show() => Agava.YandexGames.InterstitialAd.Show(
            OnOpenCallback,
            OnCloseCallback,
            OnErrorCallback,
            OnOfflineCallback);

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            _mainMusic.Stop();
            _mainMusic.volume = 0;
            OnOpenAd.Invoke();
        }

        private void OnCloseCallback(bool isWorking)
        {
            if (isWorking)
            {
                Time.timeScale = 0;
                OnCloseAd?.Invoke();
                return;
            }

            OnCloseAd?.Invoke();
        }

        private void OnErrorCallback(string errorMessage)
        {
            Time.timeScale = 0;
            OnErrorAd?.Invoke();
        }

        private void OnOfflineCallback()
        {
            Time.timeScale = 0;
        }
    }
}