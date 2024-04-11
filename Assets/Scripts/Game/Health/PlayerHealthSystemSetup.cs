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
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Vignette _vignette;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _reliveButton;
    [SerializeField] private int _healthCount;

    public HealthSystem HealthSystem;
    private PlayerHealthSystemPresenter _presenter;
    private VideoAd _videoAd;

    private void Awake()
    {
        _videoAd = GetComponent<VideoAd>();
        HealthSystem = new HealthSystem(_healthCount);
        _presenter = new PlayerHealthSystemPresenter(_playerTakeDamage, _videoAd, _reliveButton, _firstAidButton, HealthSystem, _playerDied, _healthBar, _vignette);
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