using System.Collections.Generic;
using System.Linq;
using Game.Collision;
using Game.Health;
using Game.Player.Levels;
using Game.UI;
using Game.UI.Screens;
using Game.Zombie;
using UnityEngine;
using YandexElements;

namespace Game.ObjectPools.ZombiePools
{
    public class ZombiePool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _capacity;
        [SerializeField] private int _healthValue;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Window> _windows;
        [SerializeField] private VideoAd _videoAd;
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private GameTime _gameTime;

        protected List<GameObject> Pool;
        protected List<CharacterHealthPresenter> HealthSystemPresenters;
        protected List<ExperiencePresenter> ExperienceSystemPresenters;

        private void Awake()
        {
            Pool = new List<GameObject>();
            HealthSystemPresenters = new List<CharacterHealthPresenter>();
            ExperienceSystemPresenters = new List<ExperiencePresenter>();
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
            _interstitialAd.OnOpenAd -= StopSound;
            _videoAd.OnCloseAd -= PlaySound;

            foreach (var presenter in HealthSystemPresenters)
            {
                presenter.Disable();
            }

            foreach (ExperiencePresenter presenter in ExperienceSystemPresenters)
            {
                presenter.Disable();
            }
        }

        public void CreateZombies(Level levelSystemModel)
        {
            for (int i = 0; i < _capacity; i++)
            {
                GameObject zombie = Instantiate(_prefab, _container);

                Health.Health healthSystemModel = new Health.Health(_healthValue);
                CharacterHealthPresenter healthSystemPresenter = new CharacterHealthPresenter(
                    healthSystemModel,
                    zombie.transform.Find("Collider")
                .GetComponent<ZombieCollisionHandler>(),
                zombie.GetComponent<ZombieDead>(),
                zombie.GetComponent<EnemyBlink>());
                HealthSystemPresenters.Add(healthSystemPresenter);

                ExperiencePresenter experienceSystemPresenter = new ExperiencePresenter(
                    levelSystemModel,
                    zombie.GetComponent<ZombieDead>(),
                    zombie.GetComponent<Experience>());
                ExperienceSystemPresenters.Add(experienceSystemPresenter);

                zombie.SetActive(false);
                Pool.Add(zombie);
            }

            foreach (CharacterHealthPresenter presenter in HealthSystemPresenters)
            {
                presenter.Enable();
            }

            foreach (ExperiencePresenter presenter in ExperienceSystemPresenters)
            {
                presenter.Enable();
            }
        }

        public bool TryGetObject(out GameObject result)
        {
            result = Pool.FirstOrDefault(p => p.activeSelf == false);
            result.GetComponent<ZombieDead>().ReviveZombie();

            return result != null;
        }

        public void DisableSound()
        {
            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();
                audio.enabled = false;
            }
        }

        public void EnableSound()
        {
            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();
                audio.enabled = true;
            }
        }

        public void PauseSound()
        {
            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();
                audio.Pause();
                audio.volume = 0;
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

            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();
                audio.volume = 1;
            }

            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();

                if (audio.time != 0 && audio.enabled == true)
                {
                    audio.Play();
                    return;
                }
            }
        }

        public void StopSound()
        {
            foreach (GameObject zombie in Pool)
            {
                AudioSource audio = zombie.GetComponent<AudioSource>();

                audio.Stop();
            }
        }
    }
}