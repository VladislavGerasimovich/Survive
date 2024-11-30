using Game.Buttons;
using Game.Player;
using Game.UI;
using Localization;
using Menu.Shop;
using Storage;
using UnityEngine;
using YandexElements;

namespace Game.Health
{
    [RequireComponent(typeof(VideoAd))]
    [RequireComponent(typeof(PlayerDataManager))]
    public class PlayerHealthSetup : MonoBehaviour
    {
        [SerializeField] private PlayerTakeDamage _playerTakeDamage;
        [SerializeField] private PlayerDeath _playerDied;
        [SerializeField] private PlayerMortality _playerMortality;
        [SerializeField] private ImmortalityCount _immortalityCount;
        [SerializeField] private FirstAidKitCount _firstAidKitCount;
        [SerializeField] private Bar _healthBar;
        [SerializeField] private Vignette _vignette;
        [SerializeField] private PressButton _firstAidButton;
        [SerializeField] private PressButton _reliveButton;
        [SerializeField] private PressButton _immortalityButton;
        [SerializeField] private int _healthCount;
        [SerializeField] private PopUpWindowForGame _immortalityPopUpWindow;
        [SerializeField] private PopUpWindowForGame _firstAidPopUpWindow;
        [SerializeField] private Language _immortalityText;
        [SerializeField] private Language _firstAidText;

        public Health HealthSystem;

        private PlayerDataManager _playerDataManager;
        private PlayerHealthPresenter _presenter;
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

            HealthSystem = new Health(_healthCount);
            _presenter = new PlayerHealthPresenter(
                _firstAidKitCount,
                _immortalityCount,
                _playerMortality,
                _playerTakeDamage,
                _videoAd,
                _reliveButton,
                _firstAidButton,
                _immortalityButton,
                HealthSystem,
                _playerDied,
                _healthBar,
                _vignette,
                _immortalityPopUpWindow,
                _firstAidPopUpWindow);
            _presenter.Enable();
        }
    }
}