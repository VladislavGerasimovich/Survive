using UnityEngine;

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
    private PopUpWindowForGame _immortalityPopUpWindow;
    private PopUpWindowForGame _firstAidPopUpWindow;
    private int _countOfClick;

    public PlayerHealthSystemPresenter(FirstAidKitCount firstAidKitCount, ImmortalityCount immortalityCount, PlayerMortality playerMortality, PlayerTakeDamage playerTakeDamage,VideoAd videoAd, PressButton reliveButton, PressButton firstAidButton, PressButton immortalityButton, HealthSystem healthSystem, PlayerDied died, HealthBar healthBar, Vignette vignette, PopUpWindowForGame immortalityPopUpWindow, PopUpWindowForGame firstAidPopUpWindow)
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
        _immortalityPopUpWindow = immortalityPopUpWindow;
        _firstAidPopUpWindow = firstAidPopUpWindow;
    }

    public void Enable()
    {
        _immortalityPopUpWindow.YesButtonClicked += ShowAdForImmortality;
        _immortalityPopUpWindow.NoButtonClicked += OnNoButtonClicked;
        _firstAidPopUpWindow.YesButtonClicked += ShowAdForFirstAid;
        _firstAidPopUpWindow.NoButtonClicked += OnNoButtonClickedFirstAidPopUpWindow;
        _reliveButton.Click += OnReliveButtonClick;
        _firstAidButton.Click += OnFirstAidButtonClick;
        _immortalityButton.Click += OnImmortalityButtonClick;
        _healthSystem.OnValueChanged += ChangeSliderValue;
        _healthSystem.Died += OnDied;
        _playerTakeDamage.TakeDamage += OnTakeDamage;
    }

    public void Disable()
    {
        _immortalityPopUpWindow.YesButtonClicked -= ShowAdForImmortality;
        _immortalityPopUpWindow.NoButtonClicked -= OnNoButtonClicked;
        _firstAidPopUpWindow.YesButtonClicked -= ShowAdForFirstAid;
        _firstAidPopUpWindow.NoButtonClicked -= OnNoButtonClickedFirstAidPopUpWindow;
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
        if(_healthSystem.IsImmortal == true)
        {
            return;
        }

        _firstAidButton.StatusInteractableOn();
        _firstAidButton.InteractableOn();
        _healthSystem.TakeDamage(damage);
    }

    private void OnDied()
    {
        _playerDied.Died();
    }

    private void OnNoButtonClicked()
    {
        _immortalityButton.InteractableOn();
    }

    private void OnNoButtonClickedFirstAidPopUpWindow()
    {
        _firstAidButton.InteractableOn();
    }

    private void OnFirstAidButtonClick()
    {
        _countOfClick++;
        _firstAidButton.InteractableOff();
        bool isGreaterThanZero = _firstAidKitCount.IsCountGreaterThenZero();

        if (isGreaterThanZero == false)
        {
            _firstAidPopUpWindow.Open();

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
        _immortalityButton.InteractableOff();
        bool isGreaterThanZero = _immortalityCount.IsCountGreaterThenZero();

        if(isGreaterThanZero == false)
        {
            _immortalityPopUpWindow.Open();

            return;
        }
        else
        {
            _immortalityCount.ReduceCount();
            MakeImmortal();
        }
    }

    private void ShowAdForImmortality()
    {
        _videoAd.Show();
        _videoAd.OnRewardReceived += MakeImmortal;
        _videoAd.OnCloseAd += CloseImmortalityPopUpWindow;
    }

    private void ShowAdForFirstAid()
    {
        _videoAd.Show();
        _videoAd.OnRewardReceived += RestoreAllHealth;
        _videoAd.OnCloseAd += CloseFirstAidPopUpWindow;
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
        _healthSystem.MakeMortal();
        _playerMortality.IsMortal -= MakeMortal;
    }

    private void CloseImmortalityPopUpWindow()
    {
        _immortalityPopUpWindow.Close();
        _videoAd.OnRewardReceived -= MakeImmortal;
        _videoAd.OnRewardReceived -= RestoreAllHealth;
        _videoAd.OnCloseAd -= CloseImmortalityPopUpWindow;

        if(_videoAd.IsRewardReseived == false)
        {
            _immortalityButton.InteractableOn();
        }
    }

    private void CloseFirstAidPopUpWindow()
    {
        _firstAidPopUpWindow.Close();
        _videoAd.OnRewardReceived -= MakeImmortal;
        _videoAd.OnRewardReceived -= RestoreAllHealth;
        _videoAd.OnCloseAd -= CloseFirstAidPopUpWindow;

        if (_videoAd.IsRewardReseived == false)
        {
            _firstAidButton.InteractableOn();
        }
    }
}
