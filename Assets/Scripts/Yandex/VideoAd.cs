using Game;
using UnityEngine;
using UnityEngine.Events;

namespace YandexElements
{
    [RequireComponent(typeof(AudioSource))]
    public class VideoAd : MonoBehaviour
    {
        [SerializeField] private AudioSource _shotSound;
        [SerializeField] private GameTime _gameTime;

        private AudioSource _mainMusic;

        public event UnityAction OnOpenAd;
        public event UnityAction OnRewardReceived;
        public event UnityAction OnCloseAd;

        public bool IsRewardReseived { get; private set; }
        public bool IsOpen { get; private set; }

        private void Awake()
        {
            _mainMusic = GetComponent<AudioSource>();
        }

        public void Show() =>
            Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

        public void OnOpenCallback()
        {
            IsRewardReseived = false;
            AudioListener.volume = 0;

            if (_gameTime != null)
            {
                _gameTime.Stop();
            }

            IsOpen = true;
            _mainMusic.Pause();

            if (_shotSound != null)
            {
                _shotSound.Stop();
            }

            OnOpenAd.Invoke();
        }

        public void OnRewardCallback()
        {
            OnRewardReceived.Invoke();
            IsRewardReseived = true;
        }

        public void OnCloseCallback()
        {
            IsOpen = false;
            OnCloseAd.Invoke();

            if (_gameTime != null)
            {
                _gameTime.Run();
            }

            AudioListener.volume = 1;

            if (_mainMusic.enabled == true)
            {
                _mainMusic.Play();
            }

            if (_shotSound.time != 0 && _shotSound.enabled == true && _shotSound != null)
            {
                _shotSound.Play();
            }
        }
    }
}