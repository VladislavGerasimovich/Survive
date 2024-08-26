using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    protected List<CharacterHealthSystemPresenter> HealthSystemPresenters;
    protected List<ExperienceSystemPresenter> ExperienceSystemPresenters;

    private void Awake()
    {
        Pool = new List<GameObject>();
        HealthSystemPresenters = new List<CharacterHealthSystemPresenter>();
        ExperienceSystemPresenters = new List<ExperienceSystemPresenter>();
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

        foreach (ExperienceSystemPresenter presenter in ExperienceSystemPresenters)
        {
            presenter.Disable();
        }
    }

    public void CreateZombies(LevelSystem levelSystemModel)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject zombie = Instantiate(_prefab, _container);

            HealthSystem healthSystemModel = new HealthSystem(_healthValue);
            CharacterHealthSystemPresenter healthSystemPresenter = new CharacterHealthSystemPresenter(healthSystemModel, zombie.transform.Find("Collider").GetComponent<ZombieCollisionHandler>(), zombie.GetComponent<ZombieDied>(), zombie.GetComponent<EnemyBlink>());
            HealthSystemPresenters.Add(healthSystemPresenter);

            ExperienceSystemPresenter experienceSystemPresenter = new ExperienceSystemPresenter(levelSystemModel, zombie.GetComponent<ZombieDied>(), zombie.GetComponent<Experience>());
            ExperienceSystemPresenters.Add(experienceSystemPresenter);

            zombie.SetActive(false);
            Pool.Add(zombie);
        }

        foreach (CharacterHealthSystemPresenter presenter in HealthSystemPresenters)
        {
            presenter.Enable();
        }

        foreach (ExperienceSystemPresenter presenter in ExperienceSystemPresenters)
        {
            presenter.Enable();
        }
    }

    public bool TryGetObject(out GameObject result)
    {
        result = Pool.FirstOrDefault(p => p.activeSelf == false);
        result.GetComponent<ZombieDied>().ReviveZombie();

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
            if(window.IsOpen == true)
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