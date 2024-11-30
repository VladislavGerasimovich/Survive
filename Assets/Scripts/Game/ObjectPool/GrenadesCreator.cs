using System.Collections.Generic;
using Game.UI.Screens;
using UnityEngine;
using YandexElements;

namespace Game.ObjectPools
{
    public class GrenadesCreator : ObjectPool
    {
        [SerializeField] private GameObject _grenade;
        [SerializeField] private List<Window> _windows;
        [SerializeField] private VideoAd _videoAd;
        [SerializeField] private InterstitialAd _interstitialAd;

        private void Awake()
        {
            Pool = new List<GameObject>();
            Initialize(_grenade);
        }

        private void OnEnable()
        {
            foreach (var window in _windows)
            {
                window.IsPanelOpen += PauseSound;
                window.IsPanelClose += PlaySound;
            }

            _videoAd.OnOpenAd += PauseSound;
            _interstitialAd.OnOpenAd += PauseSound;
            _videoAd.OnCloseAd += PlaySound;
        }

        private void OnDisable()
        {
            foreach (var window in _windows)
            {
                window.IsPanelOpen -= PauseSound;
                window.IsPanelClose -= PlaySound;
            }

            _videoAd.OnOpenAd -= PauseSound;
            _interstitialAd.OnOpenAd -= PauseSound;
            _videoAd.OnCloseAd -= PlaySound;
        }

        protected override void Initialize(GameObject prefab)
        {
            for (int i = 0; i < Capacity; i++)
            {
                GameObject item = Instantiate(prefab, Container.transform);
                item.SetActive(false);
                Pool.Add(item);
            }
        }

        public void PauseSound()
        {
            foreach (GameObject item in Pool)
            {
                AudioSource audio = item.GetComponent<AudioSource>();
                audio.volume = 0;

                if (audio.isPlaying)
                {
                    audio.Pause();
                }
            }
        }

        public void PlaySound()
        {
            foreach (var window in _windows)
            {
                if (window.IsOpen == true)
                {
                    return;
                }
            }

            foreach (GameObject item in Pool)
            {
                AudioSource audio = item.GetComponent<AudioSource>();
                audio.volume = 1;

                if (audio.time != 0 && audio.enabled == true)
                {
                    audio.Play();
                    return;
                }
            }
        }

        public void DisableSound()
        {
            foreach (GameObject item in Pool)
            {
                AudioSource audio = item.GetComponent<AudioSource>();
                audio.enabled = false;
            }
        }

        public void EnableSound()
        {
            foreach (GameObject item in Pool)
            {
                AudioSource audio = item.GetComponent<AudioSource>();
                audio.enabled = true;
            }
        }
    }
}