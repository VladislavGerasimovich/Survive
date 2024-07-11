using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Agava.YandexGames;

public class ImprovementSystemPresenter: MonoBehaviour
{
    private WeaponLifeCircle _melleWeapon;
    private RangeWeaponLifeCyrcle _rangeWeapon;
    private ThrowingWeapon _throwingWeapon;
    private ImprovementPanel _improvementPanel;
    private List<Improvement> _improvements;
    private List<ImprovementCard> _improvementCards;
    private WeaponDamage[] _melleWeaponDamage;
    private WeaponLifeCircle _melleWeaponLifeCircle;
    private BulletsContainer _bulletsContainer;
    private WeaponLifeCircle _rangeWeaponLifeCircle;
    private ExplosionAreaContainer _explosionAreaContainer;
    private GrenadeTrajectory[] _grenadesTrajectory;
    private int _countOfCards;
    private GameTime _gameTime;
    private int _maxSelectedCards;
    private int _currentCountOfSelectedCards;
    private PressButton _twoCardsButton;
    private PressButton _shuffleCardsButton;
    private PressButton _firstAidButton;
    private PressButton _menuButton;
    private HealthSystem _healthSystem;
    private bool _isFirstFilling;
    private bool _isFilling;
    private int _countOfUnfilledPanel;
    private VideoAdForImprovement _videoAdForIncreaseMaxCountOfSelectedCards;
    private VideoAdForImprovement _videoAdForShuffleCards;

    public ImprovementSystemPresenter(
        VideoAdForImprovement videoAdForIncreaseMaxCountOfCards,
        VideoAdForImprovement videoAdForShuffleCards,
        HealthSystem healthSystem,
        PressButton shuffleCardsButton,
        PressButton twoCardsButton,
        PressButton firstAidButton,
        PressButton menuButton,
        WeaponLifeCircle melleWeapon,
        RangeWeaponLifeCyrcle rangeWeapon,
        ThrowingWeapon throwingWeapon,
        GameTime gameTime, 
        ImprovementPanel improvementPanel,
        List<Improvement> improvements,
        List<ImprovementCard> improvementCards,
        WeaponDamage[] melleWeaponDamage,
        int count,
        WeaponLifeCircle melleWeaponLifeCircle,
        BulletsContainer bulletsContainer,
        WeaponLifeCircle rangeWeaponLifeCircle,
        ExplosionAreaContainer explosionAreaContainer,
        GrenadeTrajectory[] grenadesTrajectory
        )
    {
        _videoAdForIncreaseMaxCountOfSelectedCards = videoAdForIncreaseMaxCountOfCards;
        _videoAdForShuffleCards = videoAdForShuffleCards;
        _healthSystem = healthSystem;
        _shuffleCardsButton = shuffleCardsButton;
        _twoCardsButton = twoCardsButton;
        _firstAidButton = firstAidButton;
        _menuButton = menuButton;
        _melleWeapon = melleWeapon;
        _rangeWeapon = rangeWeapon;
        _throwingWeapon = throwingWeapon;
        _countOfCards = count;
        _improvementPanel = improvementPanel;
        _improvements = improvements;
        _improvementCards = improvementCards;
        _melleWeaponDamage = melleWeaponDamage;
        _gameTime = gameTime;
        _melleWeaponLifeCircle = melleWeaponLifeCircle;
        _bulletsContainer = bulletsContainer;
        _rangeWeaponLifeCircle = rangeWeaponLifeCircle;
        _explosionAreaContainer = explosionAreaContainer;
        _grenadesTrajectory = grenadesTrajectory;

        _maxSelectedCards = 1;
        _isFirstFilling = true;
    }

    public void Enable()
    {
        _melleWeapon.Run();

        _twoCardsButton.Click += OnTwoCardsButtonClick;
        _shuffleCardsButton.Click += OnShuffleCardsButtonClick;
        _improvementPanel.IsPanelOpen += FillPanel;
        _videoAdForIncreaseMaxCountOfSelectedCards.RewardReceived += IncreaseMaxCountOfSelectedCards;
        _videoAdForIncreaseMaxCountOfSelectedCards.OnCloseAd += SetPanelAfterIncreaseMaxCountOfSelectedCards;
        _videoAdForShuffleCards.RewardReceived += ShuffleCards;
        _videoAdForShuffleCards.OnCloseAd += SetPanelAfterShuffleCards;

        foreach (var improvement in _improvements)
        {
            improvement.LevelIncreased += OnLevelIncreased;
            improvement.MaxLevelReached += ProhibitUseCard;
        }

        foreach (var improvementCard in _improvementCards)
        {
            improvementCard.Click += OnClick;
        }
    }

    public void Disable()
    {
        _twoCardsButton.Click -= OnTwoCardsButtonClick;
        _shuffleCardsButton.Click -= OnShuffleCardsButtonClick;
        _improvementPanel.IsPanelOpen -= Fill;
        _videoAdForIncreaseMaxCountOfSelectedCards.RewardReceived -= IncreaseMaxCountOfSelectedCards;
        _videoAdForIncreaseMaxCountOfSelectedCards.OnCloseAd -= SetPanelAfterIncreaseMaxCountOfSelectedCards;
        _videoAdForShuffleCards.RewardReceived -= ShuffleCards;
        _videoAdForShuffleCards.OnCloseAd -= SetPanelAfterShuffleCards;

        foreach (var improvement in _improvements)
        {
            improvement.LevelIncreased -= OnLevelIncreased;
            improvement.MaxLevelReached -= ProhibitUseCard;
        }

        foreach (var improvementCard in _improvementCards)
        {
            improvementCard.Click -= OnClick;
        }
    }


    private void FillPanel()
    {
        if (_isFilling == false)
        {
            Fill();
        }
        else
        {
            _countOfUnfilledPanel++;
        }
    }

    private void OnShuffleCardsButtonClick()
    {
        foreach (ImprovementCard card in _improvementCards)
        {
            card.DisableButton();
        }

        _twoCardsButton.InteractableOff();
        _shuffleCardsButton.InteractableOff();
        _menuButton.InteractableOff();
        _videoAdForShuffleCards.Show();
    }

    private void ShuffleCards()
    {
        foreach (ImprovementCard improvementCard in _improvementCards)
        {
            improvementCard.SetParent();
        }

        Fill();
    }

    private void SetPanelAfterShuffleCards()
    {
        foreach (ImprovementCard card in _improvementCards)
        {
            card.EnableButton();
        }

        if (_twoCardsButton.Interactable == true)
        {
            _twoCardsButton.InteractableOn();
        }

        if (_shuffleCardsButton.Interactable == true)
        {
            _shuffleCardsButton.InteractableOn();
        }

        _menuButton.InteractableOn();
    }

    private void OnTwoCardsButtonClick()
    {
        foreach (ImprovementCard card in _improvementCards)
        {
            card.DisableButton();
        }

        _twoCardsButton.InteractableOff();
        _shuffleCardsButton.InteractableOff();
        _menuButton.InteractableOff();
        _videoAdForIncreaseMaxCountOfSelectedCards.Show();
    }

    private void IncreaseMaxCountOfSelectedCards()
    {
        _maxSelectedCards = 2;
        _twoCardsButton.StatusInteractableOff();
        _twoCardsButton.InteractableOff();
    }

    private void SetPanelAfterIncreaseMaxCountOfSelectedCards()
    {
        foreach (ImprovementCard card in _improvementCards)
        {
            card.EnableButton();
        }

        if (_shuffleCardsButton.Interactable == true)
        {
            _shuffleCardsButton.InteractableOn();
        }

        if(_twoCardsButton.Interactable == true)
        {
            _twoCardsButton.InteractableOn();
        }
        UnityEngine.Debug.Log(_twoCardsButton.Interactable + " twocardsbuttonINTERACTABLE");

        _menuButton.InteractableOn();
    }

    private void InteractableOnTwoCardsButton()
    {
        _twoCardsButton.InteractableOn();
    }

    private void Fill()
    {
        _isFilling = true;

        if (_isFirstFilling == true)
        {
            InteractableOnTwoCardsButton();
            _isFirstFilling = false;
        }

        if(_twoCardsButton.Interactable == true)
        {
            _twoCardsButton.InteractableOn();
        }

        if (_shuffleCardsButton.Interactable == true)
        {
            _shuffleCardsButton.InteractableOn();
        }
        

        List<ImprovementCard> selectedCards = new List<ImprovementCard>();

        for (int i = 0; i < _countOfCards; i++)
        {
            GetRandomCard(out ImprovementCard improvementCard, selectedCards);

            if (improvementCard != null)
            {
                selectedCards.Add(improvementCard);
                improvementCard.gameObject.transform.SetParent(_improvementPanel.transform);
                improvementCard.transform.localScale = Vector3.one;
            }
        }
    }
    
    private void GetRandomCard(out ImprovementCard improvementCard, List<ImprovementCard> selectedCards)
    {
        improvementCard = null;
        List<ImprovementCard> cards = _improvementCards.Where(card => card.CanUse == true).ToList();

        for (int i = 0; i < selectedCards.Count; i++)
        {
            cards.Remove(selectedCards[i]);
        }

        if(cards.Count > 0)
        {
            int randomNumber = UnityEngine.Random.Range(0, cards.Count);
            improvementCard = cards[randomNumber];
        }
    }

    private void OnClick(string type)
    {
        _shuffleCardsButton.InteractableOff();
        _currentCountOfSelectedCards++;

        foreach (Improvement improvement in _improvements)
        {
            if(improvement.Type == type)
            {
                improvement.IncreaseLevel();
            }
        }

        if(type == "RangeWeapon")
        {
            _rangeWeapon.Run();
            ProhibitUseCard(type);
        }
        else if(type == "ThrowingWeapon")
        {
            _throwingWeapon.Run();
            ProhibitUseCard(type);
        }
        else if(type == "FirstAid")
        {
            foreach (ImprovementCard improvementCard in _improvementCards)
            {
                if (improvementCard.Type == "FirstAid")
                {
                    _healthSystem.Restore();
                    _firstAidButton.StatusInteractableOff();
                    _firstAidButton.InteractableOff();
                }
            }
        }

        if (_currentCountOfSelectedCards == _maxSelectedCards)
        {
            CloseImprovementPanel();
            _currentCountOfSelectedCards = 0;
            _maxSelectedCards = 1;
            _gameTime.Run();
        }
    }

    private void OnLevelIncreased(int value, string type)
    {
        if(type == "MelleDamage")
        {
            foreach (WeaponDamage melleWeaponDamage in _melleWeaponDamage)
            {
                melleWeaponDamage.SetDamage(value);
            }
        }
        else if(type == "MelleWeaponReloading")
        {
            _melleWeaponLifeCircle.ChangeDurationOfReloading(value);
        }
        else if (type == "RangeDamage")
        {
            _bulletsContainer.SetPoolObjectDamage(value);
        }
        else if (type == "RangeWeaponReloading")
        {
            _rangeWeaponLifeCircle.ChangeDurationOfReloading(value);
        }
        else if (type == "ExplosionDamage")
        {
            _explosionAreaContainer.SetPoolObjectDamage(value);
        }
        else if (type == "ExplosionReloading")
        {
            foreach (GrenadeTrajectory grenadeTrajectory in _grenadesTrajectory)
            {
                grenadeTrajectory.ChangeDurationOfReloading(value);
            }
        }
    }

    private void ProhibitUseCard(string type)
    {
        foreach (ImprovementCard improvementCard in _improvementCards)
        {
            if(improvementCard.Type == type)
            {
                improvementCard.ProhibitUse();
            }
        }
    }

    private void CloseImprovementPanel()
    {
        _twoCardsButton.InteractableOff();
        _shuffleCardsButton.InteractableOff();
        _improvementPanel.Close();

        foreach (ImprovementCard improvementCard in _improvementCards)
        {
            improvementCard.EnableButton();
            improvementCard.SetParent();
        }

        List<ImprovementCard> improvementCards = _improvementCards.Where(card => card.CanUse == true).ToList();

        if(improvementCards.Count <= _countOfCards)
        {
            _shuffleCardsButton.StatusInteractableOff();
        }

        if(improvementCards.Count <= 1)
        {
            _twoCardsButton.StatusInteractableOff();
        }
        else if (improvementCards.Count > 1)
        {
            _twoCardsButton.StatusInteractableOn();
        }

        if (improvementCards.Count <= 0)
        {
            foreach (ImprovementCard improvementCard in _improvementCards)
            {
                if(improvementCard.Type == "FirstAid")
                {
                    improvementCard.Allowuse();
                }
            }
        }

        _isFilling = false;

        if(_countOfUnfilledPanel > 0)
        {
            _countOfUnfilledPanel--;
            _improvementPanel.Open();
        }
    }
}