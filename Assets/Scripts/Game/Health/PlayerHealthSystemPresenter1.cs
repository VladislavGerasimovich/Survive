using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystemPresenter
{
    private VideoAd _videoAd;
    private HealthSystem _healthSystem;
    private ExperienceBar _healthBar;
    private PlayerDied _playerDied;
    private Vignette _vignette;
    private PressButton _firstAidButton;
    private PressButton _reliveButton;
    private PlayerTakeDamage _playerTakeDamage;

    public PlayerHealthSystemPresenter(PlayerTakeDamage playerTakeDamage,VideoAd videoAd, PressButton reliveButton, PressButton firstAidButton, HealthSystem healthSystem, PlayerDied died, HealthBar healthBar, Vignette vignette)
    {
        _playerTakeDamage = playerTakeDamage;
        _videoAd = videoAd;
        _reliveButton = reliveButton;
        _firstAidButton = firstAidButton;
        _healthSystem = healthSystem;
        _playerDied = died;
        _healthBar = healthBar;
        _vignette = vignette;
    }

    public void Enable()
    {
        _reliveButton.GetComponent<Button>().onClick.AddListener(OnRelive);
        _firstAidButton.Click += RestoreHealth;
        _healthSystem.OnValueChanged += ChangeSliderValue;
        _healthSystem.Died += OnDied;
        _playerTakeDamage.TakeDamage += OnTakeDamage;
    }

    public void Disable()
    {
        _firstAidButton.Click -= RestoreHealth;
        _healthSystem.OnValueChanged -= ChangeSliderValue;
        _healthSystem.Died -= OnDied;
        _playerTakeDamage.TakeDamage -= OnTakeDamage;
    }

    private void ChangeSliderValue(float value)
    {
        _healthBar.OnValueChanged(value);

        if(value < 1)
        {
            _vignette.StartShowVignetteCoroutine();
        }
    }

    private void RestoreHealth()
    {
        _videoAd.Show();
        RestoreAllHealth();
    }

    private void RestoreAllHealth()
    {
        _healthSystem.Restore();

        _firstAidButton.StatusInteractableOff();
        _firstAidButton.InteractableOff();
    }

    private void OnTakeDamage(int damage)
    {
        _firstAidButton.StatusInteractableOn();
        _firstAidButton.InteractableOn();
        _healthSystem.TakeDamage(damage);
    }

    private void OnDied()
    {
        _playerDied.Died();
    }

    private void OnRelive()
    {
        _healthSystem.Restore();
    }
}
