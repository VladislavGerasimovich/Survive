using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Shop.Items
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class Item : MonoBehaviour
    {
        [SerializeField] protected TMP_Text InfoText;
        [SerializeField] protected List<string> Cost;
        [SerializeField] protected string Type;
        [SerializeField] protected PopUpWindow PopUpWindow;
        [SerializeField] protected PlayerDataManager _playerDataManager;
        [SerializeField] protected List<Color> Colors;
        [SerializeField] private string _englishText;
        [SerializeField] private string _turkishText;
        [SerializeField] private string _russianText;

        protected Image Background;
        protected Button Button;
        protected int CostCountMultiplier;

        private int _indexOfCost;

        public virtual event Action<string, string> Clicked;

        public string TranslatedText { get; private set; }
        public string Class { get; protected set; }

        private void Awake()
        {
            Background = GetComponent<Image>();
            Button = GetComponent<Button>();
            Class = Type;
            string languageCode = YandexGamesSdk.Environment.i18n.lang;
            ChangeLanguage(languageCode);
            CostCountMultiplier = 1;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
            _playerDataManager.DataReceived += SetIndex;
        }

        private void Start()
        {
            if(Type != Constants.Money)
            {
                SetCost();
            }
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnClick);
            _playerDataManager.DataReceived -= SetIndex;
        }

        public void ChangeLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case Constants.English:
                    TranslatedText = _englishText;
                    break;
                case Constants.Turkish:
                    TranslatedText = _turkishText;
                    break;
                case Constants.Russian:
                    TranslatedText = _russianText;
                    break;
            }
        }

        public virtual void SetCost()
        {
            InfoText.text = Cost[_indexOfCost];
            Background.color = Colors[_indexOfCost];

            if (_indexOfCost == Cost.Count - CostCountMultiplier)
            {
                Button.interactable = false;
            }
        }

        public virtual void SetStatus()
        {
            if (_indexOfCost < Cost.Count)
            {
                _indexOfCost++;

                if (_indexOfCost != Cost.Count)
                {
                    SetCost();
                }

                if (_indexOfCost == Cost.Count - CostCountMultiplier)
                {
                    Button.interactable = false;
                }
            }

            _playerDataManager.Set(Type, _indexOfCost);
        }

        public virtual void OnClick()
        {
            Clicked?.Invoke(Cost[_indexOfCost], Type);
        }

        protected virtual void SetIndex(PlayerData playerData)
        {
            switch (Type)
            {
                case Constants.BodyArmor:
                    _indexOfCost = playerData.BodyArmorIndex;
                    break;
                case Constants.Boots:
                    _indexOfCost = playerData.BootsIndex;
                    break;
                case Constants.Helmet:
                    _indexOfCost = playerData.HelmetIndex;
                    break;
                case Constants.MelleWeaponDamage:
                    _indexOfCost = playerData.MelleWeaponDamageIndex;
                    break;
                case Constants.MelleWeaponReloading:
                    _indexOfCost = playerData.MelleWeaponReloadingIndex;
                    break;
                case Constants.RangeWeaponDamage:
                    _indexOfCost = playerData.RangeWeaponDamageIndex;
                    break;
                case Constants.RangeWeaponReloading:
                    _indexOfCost = playerData.RangeWeaponReloadingIndex;
                    break;
                case Constants.ThrowingWeaponDamage:
                    _indexOfCost = playerData.ThrowingWeaponDamageIndex;
                    break;
                case Constants.ThrowingWeaponReloading:
                    _indexOfCost = playerData.ThrowingWeaponReloadingIndex;
                    break;
            }

            SetCost();
        }
    }
}