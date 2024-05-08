using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VideoAd))]
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

    private const string HELMET = "HELMET";
    private const string BODY_ARMOR = "BODY_ARMOR";
    private const string BOOTS = "BOOTS";

    public HealthSystem HealthSystem;
    private PlayerHealthSystemPresenter _presenter;
    private VideoAd _videoAd;

    private void Awake()
    {
        _healthCount += PlayerPrefs.GetInt(HELMET);
        _healthCount += PlayerPrefs.GetInt(BODY_ARMOR);
        _healthCount += PlayerPrefs.GetInt(BOOTS);
        Debug.Log(_healthCount + " allHealth");

        _videoAd = GetComponent<VideoAd>();
        HealthSystem = new HealthSystem(_healthCount);
        _presenter = new PlayerHealthSystemPresenter(_firstAidKitCount, _immortalityCount, _playerMortality, _playerTakeDamage, _videoAd, _reliveButton, _firstAidButton, _immortalityButton, HealthSystem, _playerDied, _healthBar, _vignette);
    }

    private void OnEnable()
    {
        _presenter.Enable();
    }

    private void OnDisable()
    {
        _presenter.Disable();
    }
}