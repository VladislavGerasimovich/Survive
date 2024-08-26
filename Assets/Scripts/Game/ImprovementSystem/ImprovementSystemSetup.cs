using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using Agava.YandexGames;

[RequireComponent(typeof(VideoAdForImprovement))]
[RequireComponent(typeof(PlayerDataManager))]
public class ImprovementSystemSetup : MonoBehaviour
{
    [SerializeField] private Improvements _melleWeaponDamage;
    [SerializeField] private int[] _melleDamageValues;
    [SerializeField] private WeaponDamage[] _melleWeaponsDamage;
    [SerializeField] private Improvements _melleWeaponReloading;
    [SerializeField] private int[] _MelleWeaponReloadingValues;
    [SerializeField] private WeaponLifeCircle _weaponLifeCircle;
    [SerializeField] private Improvements _throwingWeaponDamage;
    [SerializeField] private int[] _throwingDamageValues;
    [SerializeField] private ExplosionAreaContainer _explosionAreaContainer;
    [SerializeField] private Improvements _throwingWeaponReloading;
    [SerializeField] private int[] _ThrowingWeaponReloadingValues;
    [SerializeField] private GrenadeTrajectory[] _grenadesTrajectory;
    [SerializeField] private Improvements _rangeWeaponDamage;
    [SerializeField] private int[] _rangeDamageValues;
    [SerializeField] private BulletsContainer _bulletsContainer;
    [SerializeField] private Improvements _rangeWeaponReloading;
    [SerializeField] private int[] _RangeWeaponReloadingValues;
    [SerializeField] private WeaponLifeCircle _rangeWeaponLifeCircle;
    [SerializeField] private Improvements _rangeWeaponImprovement;
    [SerializeField] private Improvements _throwingWeaponImprovement;
    [SerializeField] private string _rangeWeaponType;
    [SerializeField] private string _throwingWeaponType;
    [SerializeField] private WeaponLifeCircle _melleWeapon;
    [SerializeField] private RangeWeaponLifeCyrcle _rangeWeaponLifeCyrcle;
    [SerializeField] private ThrowingWeapon _throwingWeapon;
    [SerializeField] private Transform _mainContainer;
    [SerializeField] private GameObject _improvementCard;
    [SerializeField] private GameObject _container;
    [SerializeField] private Sprite _firstAidIcon;
    [SerializeField] private string _firstAidCardInfo;
    [SerializeField] private string _firstAidType;
    [SerializeField] private ImprovementPanel _improvementPanel;
    [SerializeField] private int _countOfCards;
    [SerializeField] private GameTime _gameTime;
    [SerializeField] private PressButton _twoCardsButton;
    [SerializeField] private PressButton _shuffleCardsButton;
    [SerializeField] private PressButton _firstAidButton;
    [SerializeField] private PressButton _menuButton;
    [SerializeField] private PlayerHealthSystemSetup _playerHealthSystemSetup;
    [SerializeField] private Sprite _rangeWeaponSprite;
    [SerializeField] private LeanLocalization _leanLocalization;
    [SerializeField] private VideoAdForImprovement _videoAdForIncreaseMaxCountOfCards;
    [SerializeField] private VideoAdForImprovement _videoAdForShuffleCards;

    private List<Improvement> _allImprovements;
    private List<ImprovementCard> _allCards;
    private ImprovementSystemPresenter _presenter;
    private PlayerDataManager _playerDataManager;

    private void Awake()
    {
        _playerDataManager = GetComponent<PlayerDataManager>();
        _allImprovements = new List<Improvement>();
        _allCards = new List<ImprovementCard>();
        string languageCode = YandexGamesSdk.Environment.i18n.lang;
        ChangeLanguage(languageCode);

   }

    private void OnEnable()
    {
        _playerDataManager.DataReceived += Create;
    }

    private void OnDisable()
    {
        _playerDataManager.DataReceived -= Create;
        _presenter.Disable();
    }

    public void ChangeLanguage(string languageCode)
    {
        _rangeWeaponImprovement.ChangeLanguage(languageCode);
        _throwingWeaponImprovement.ChangeLanguage(languageCode);
        _melleWeaponDamage.ChangeLanguage(languageCode);
        _melleWeaponReloading.ChangeLanguage(languageCode);
        _throwingWeaponDamage.ChangeLanguage(languageCode);
        _throwingWeaponReloading.ChangeLanguage(languageCode);
        _rangeWeaponDamage.ChangeLanguage(languageCode);
        _rangeWeaponReloading.ChangeLanguage(languageCode);
    }

    private void CreateImprovement(int[] values, int _startLevel, int _maxLevel, string type)
    {
        Improvement improvement = new Improvement(values, _startLevel, _maxLevel, type);
        _allImprovements.Add(improvement);
    }

    private void CreateCard(Sprite image, string text, int countOfLevels, string type, bool canUse, Transform mainContainer)
    {
        ImprovementCard _card = Instantiate(_improvementCard, _container.transform).GetComponent<ImprovementCard>();
        _card.Render(image, text, countOfLevels, type, canUse, mainContainer);
        _allCards.Add(_card);
    }

    private void Create(PlayerData playerData)
    {
        int indexOfMelleWeaponDamage = playerData.MelleWeaponDamageIndex + 1;
        CreateImprovement(_melleDamageValues, _melleWeaponDamage.Level, indexOfMelleWeaponDamage, _melleWeaponDamage.Type);
        CreateCard(_melleWeaponDamage.Icon, _melleWeaponDamage.Text, indexOfMelleWeaponDamage, _melleWeaponDamage.Type, true, _mainContainer);

        int indexOfRangeWeaponDamage = playerData.RangeWeaponDamageIndex + 1;
        CreateImprovement(_rangeDamageValues, _rangeWeaponDamage.Level, indexOfRangeWeaponDamage, _rangeWeaponDamage.Type);
        CreateCard(_rangeWeaponDamage.Icon, _rangeWeaponDamage.Text, indexOfRangeWeaponDamage, _rangeWeaponDamage.Type, true, _mainContainer);

        int indexOfThrowingWeaponDamage = playerData.ThrowingWeaponDamageIndex + 1;
        CreateImprovement(_throwingDamageValues, _throwingWeaponDamage.Level, indexOfThrowingWeaponDamage, _throwingWeaponDamage.Type);
        CreateCard(_throwingWeaponDamage.Icon, _throwingWeaponDamage.Text, indexOfThrowingWeaponDamage, _throwingWeaponDamage.Type, true, _mainContainer);

        int indexOfMelleWeaponReloading = playerData.MelleWeaponReloadingIndex + 1;
        CreateImprovement(_MelleWeaponReloadingValues, _melleWeaponReloading.Level, indexOfMelleWeaponReloading, _melleWeaponReloading.Type);
        CreateCard(_melleWeaponReloading.Icon, _melleWeaponReloading.Text, indexOfMelleWeaponReloading, _melleWeaponReloading.Type, true, _mainContainer);

        int indexOfRangeWeaponReloading = playerData.RangeWeaponReloadingIndex + 1;
        CreateImprovement(_RangeWeaponReloadingValues, _rangeWeaponReloading.Level, indexOfRangeWeaponReloading, _rangeWeaponReloading.Type);
        CreateCard(_rangeWeaponReloading.Icon, _rangeWeaponReloading.Text, indexOfRangeWeaponReloading, _rangeWeaponReloading.Type, true, _mainContainer);

        int indexOfThrowingWeaponReloading = playerData.ThrowingWeaponReloadingIndex + 1;
        CreateImprovement(_ThrowingWeaponReloadingValues, _throwingWeaponReloading.Level, indexOfThrowingWeaponReloading, _throwingWeaponReloading.Type);
        CreateCard(_throwingWeaponReloading.Icon, _throwingWeaponReloading.Text, indexOfThrowingWeaponReloading, _throwingWeaponReloading.Type, true, _mainContainer);

        CreateCard(_rangeWeaponImprovement.Icon, _rangeWeaponImprovement.Text, 1, _rangeWeaponImprovement.Type, true, _mainContainer);
        CreateCard(_throwingWeaponImprovement.Icon, _throwingWeaponImprovement.Text, 1, _throwingWeaponImprovement.Type, true, _mainContainer);
        CreateCard(_firstAidIcon, _firstAidCardInfo, 0, _firstAidType, false, _mainContainer);

        _presenter = new ImprovementSystemPresenter(_videoAdForIncreaseMaxCountOfCards, _videoAdForShuffleCards, _playerHealthSystemSetup.HealthSystem, _shuffleCardsButton, _twoCardsButton, _firstAidButton, _menuButton, _melleWeapon, _rangeWeaponLifeCyrcle, _throwingWeapon, _gameTime, _improvementPanel, _allImprovements, _allCards, _melleWeaponsDamage, _countOfCards, _weaponLifeCircle, _bulletsContainer, _rangeWeaponLifeCircle, _explosionAreaContainer, _grenadesTrajectory);

        _presenter.Enable();
    }
}
