using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VideoAd))]
[RequireComponent(typeof(PlayerDataManager))]
public class PlayerHealthSystemSetup : MonoBehaviour
{
    [SerializeField] private PlayerTakeDamage _playerTakeDamage;
    [SerializeField] private PlayerDied _playerDied;
    [SerializeField] private PlayerMortality _playerMortality;
    [SerializeField] private ImmortalityCount _immortalityCount;
    [SerializeField] private FirstAidKitCount _firstAidKitCount;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Vignette _vignette;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _reliveButton;
    [SerializeField] private PressButton _immortalityButton;
    [SerializeField] private int _healthCount;
    [SerializeField] private PopUpWindowForGame _immortalityPopUpWindow;
    [SerializeField] private PopUpWindowForGame _firstAidPopUpWindow;
    [SerializeField] private ChangeLanguage _immortalityText;
    [SerializeField] private ChangeLanguage _firstAidText;

    public HealthSystem HealthSystem;

    private PlayerDataManager _playerDataManager;
    private PlayerHealthSystemPresenter _presenter;
    private VideoAd _videoAd;

    private void Awake()
    {
        _playerDataManager = GetComponent<PlayerDataManager>();
        _videoAd = GetComponent<VideoAd>();
    }

    private void OnEnable()
    {
        _playerDataManager.DataReceived += SetHealth;
    }

    private void OnDisable()
    {
        _playerDataManager.DataReceived -= SetHealth;
        _presenter.Disable();
    }

    private void SetHealth(PlayerData playerData)
    {
        _healthCount += playerData.HelmetIndex;
        _healthCount += playerData.BodyArmorIndex;
        _healthCount += playerData.BootsIndex;

        Debug.Log("allHealth: " + _healthCount);
        HealthSystem = new HealthSystem(_healthCount);
        _presenter = new PlayerHealthSystemPresenter(_firstAidKitCount, _immortalityCount, _playerMortality, _playerTakeDamage, _videoAd, _reliveButton, _firstAidButton, _immortalityButton, HealthSystem, _playerDied, _healthBar, _vignette, _immortalityPopUpWindow, _firstAidPopUpWindow);
        _presenter.Enable();
    }
}