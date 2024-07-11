using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystemPresenter : MonoBehaviour
{
    private VideoAd _videoAd;
    private HealthSystem _healthSystem;
    private ExperienceBar _healthBar;
    private PlayerDied _playerDied;
    private Vignette _vignette;
    private PressButton _firstAidButton;
    private PressButton _reliveButton;
    private PressButton _immortalityButton;
    private PlayerTakeDamage _playerTakeDamage;
    private PlayerMortality _playerMortality;
    private ImmortalityCount _immortalityCount;
    private FirstAidKitCount _firstAidKitCount;

    public PlayerHealthSystemPresenter(FirstAidKitCount firstAidKitCount, ImmortalityCount immortalityCount, PlayerMortality playerMortality, PlayerTakeDamage playerTakeDamage,VideoAd videoAd, PressButton reliveButton, PressButton firstAidButton, PressButton immortalityButton, HealthSystem healthSystem, PlayerDied died, HealthBar healthBar, Vignette vignette)
    {
        _firstAidKitCount = firstAidKitCount;
        _immortalityCount = immortalityCount;
        _playerMortality = playerMortality;
        _playerTakeDamage = playerTakeDamage;
        _videoAd = videoAd;
        _firstAidButton = firstAidButton;
        _reliveButton = reliveButton;
        _immortalityButton = immortalityButton;
        _healthSystem = healthSystem;
        _playerDied = died;
        _healthBar = healthBar;
        _vignette = vignette;
    }

    public void Enable()
    {
        _reliveButton.Click += OnReliveButtonClick;
        _firstAidButton.Click += OnFirstAidButtonClick;
        _immortalityButton.Click += OnImmortalityButtonClick;
        _healthSystem.OnValueChanged += ChangeSliderValue;
        _healthSystem.Died += OnDied;
        _playerTakeDamage.TakeDamage += OnTakeDamage;
    }

    public void Disable()
    {
        _reliveButton.Click -= OnReliveButtonClick;
        _firstAidButton.Click -= OnFirstAidButtonClick;
        _immortalityButton.Click -= OnImmortalityButtonClick;
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

    private void RestoreAllHealth()
    {
        _healthSystem.Restore();

        _firstAidButton.StatusInteractableOff();
        _firstAidButton.InteractableOff();
        _videoAd.OnRewardReceived -= RestoreAllHealth;
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

    private void OnFirstAidButtonClick()
    {
        bool isGreaterThanZero = _firstAidKitCount.IsCountGreaterThenZero();

        if (isGreaterThanZero == false)
        {
            Debug.Log("реклама");
            _videoAd.Show();
            _videoAd.OnRewardReceived += RestoreAllHealth;
            _videoAd.OnCloseAd += UnsubscribeEvents;
            return;
        }
        else
        {
            _firstAidKitCount.ReduceCount();
            RestoreAllHealth();
        }
    }

    private void OnImmortalityButtonClick()
    {
        bool isGreaterThanZero = _immortalityCount.IsCountGreaterThenZero();

        if(isGreaterThanZero == false)
        {
            Debug.Log("реклама");
            _videoAd.Show();
            _videoAd.OnRewardReceived += MakeImmortal;
            _videoAd.OnCloseAd += UnsubscribeEvents;
            return;
        }
        else
        {
            _immortalityCount.ReduceCount();
            MakeImmortal();
        }
    }

    private void OnReliveButtonClick()
    {
        _videoAd.OnRewardReceived += MakeImmortal;
    }

    private void MakeImmortal()
    {
        _immortalityButton.InteractableOff();
        _healthSystem.Restore();
        _healthSystem.MakeImmortal();
        _playerMortality.RunImmortalityCoroutine();
        _playerMortality.IsMortal += MakeMortal;
        _videoAd.OnRewardReceived -= MakeImmortal;
    }

    private void MakeMortal()
    {
        _immortalityButton.InteractableOn();
        _playerMortality.IsMortal -= MakeMortal;
        _healthSystem.MakeMortal();
    }

    private void UnsubscribeEvents()
    {
        _videoAd.OnRewardReceived -= MakeImmortal;
        _videoAd.OnRewardReceived -= RestoreAllHealth;
        _videoAd.OnCloseAd -= UnsubscribeEvents;
    }
}
