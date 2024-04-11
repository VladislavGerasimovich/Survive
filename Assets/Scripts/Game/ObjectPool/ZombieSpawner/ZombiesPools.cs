using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SimpleZombiePool))]
[RequireComponent(typeof(FastZombiePool))]
[RequireComponent(typeof(BigZombiePool))]
public class ZombiesPools : MonoBehaviour
{
    [SerializeField] private ExperienceBar _experienceBar;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ImprovementPanel ImprovementPanel;
    [SerializeField] private GameTime _gameTime;

    private SimpleZombiePool _simpleZombiePool;
    private FastZombiePool _fastZombiePool;
    private BigZombiePool _bigZombiePool;
    private UserInterfacePresenter _userInterfacePresenter;
    private LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = new LevelSystem();
        _simpleZombiePool = GetComponent<SimpleZombiePool>();
        _fastZombiePool = GetComponent<FastZombiePool>();
        _bigZombiePool = GetComponent<BigZombiePool>();
        _userInterfacePresenter = new UserInterfacePresenter(_levelSystem, _experienceBar, _text, ImprovementPanel, _gameTime);
    }

    private void OnEnable()
    {
        _userInterfacePresenter.Enable();
    }

    private void Start()
    {
        _simpleZombiePool.CreateZombies(_levelSystem);
        _fastZombiePool.CreateZombies(_levelSystem);
        _bigZombiePool.CreateZombies(_levelSystem);
    }

    private void OnDisable()
    {
        _userInterfacePresenter.Disable();
    }

    public void TryGetSimpleZombie(out GameObject zombie)
    {
        _simpleZombiePool.TryGetObject(out GameObject simpleZombie);
        zombie = simpleZombie;
    }

    public void TryGetFastZombie(out GameObject zombie)
    {
        _fastZombiePool.TryGetObject(out GameObject fastZombie);
        zombie = fastZombie;
    }

    public void TryGetBigZombie(out GameObject zombie)
    {
        _bigZombiePool.TryGetObject(out GameObject bigZombie);
        zombie = bigZombie;
    }
}
