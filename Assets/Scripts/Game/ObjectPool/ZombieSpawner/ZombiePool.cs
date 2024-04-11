using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _capacity;
    [SerializeField] private int _healthValue;
    [SerializeField] private Transform _container;

    protected List<GameObject> _pool;
    protected List<CharacterHealthSystemPresenter> _healthSystemPresenters;
    protected List<ExperienceSystemPresenter> _experienceSystemPresenters;

    private void Awake()
    {
        _pool = new List<GameObject>();
        _healthSystemPresenters = new List<CharacterHealthSystemPresenter>();
        _experienceSystemPresenters = new List<ExperienceSystemPresenter>();
    }

    private void OnDisable()
    {
        foreach (var presenter in _healthSystemPresenters)
        {
            presenter.Disable();
        }

        foreach (ExperienceSystemPresenter presenter in _experienceSystemPresenters)
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
            _healthSystemPresenters.Add(healthSystemPresenter);

            ExperienceSystemPresenter experienceSystemPresenter = new ExperienceSystemPresenter(levelSystemModel, zombie.GetComponent<ZombieDied>(), zombie.GetComponent<Experience>());
            _experienceSystemPresenters.Add(experienceSystemPresenter);

            zombie.SetActive(false);
            _pool.Add(zombie);
        }

        foreach (CharacterHealthSystemPresenter presenter in _healthSystemPresenters)
        {
            presenter.Enable();
        }

        foreach (ExperienceSystemPresenter presenter in _experienceSystemPresenters)
        {
            presenter.Enable();
        }
    }

    public bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}