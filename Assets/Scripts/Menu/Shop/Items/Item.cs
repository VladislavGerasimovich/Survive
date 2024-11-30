using System;
using System.Collections.Generic;
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
        private const string BODY_ARMOR = "BODY_ARMOR";
        private const string BOOTS = "BOOTS";
        private const string HELMET = "HELMET";
        private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";
        private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";
        private const string MONEY = "MONEY";
        private const string RANGE_WEAPON_DAMAGE = "RANGE_WEAPON_DAMAGE";
        private const string RANGE_WEAPON_RELOADING = "RANGE_WEAPON_RELOADING";
        private const string THROWING_WEAPON_DAMAGE = "THROWING_WEAPON_DAMAGE";
        private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";
        private const string English = "en";
        private const string Turkish = "tr";
        private const string Russian = "ru";

        [SerializeField] protected TMP_Text InfoText;
        [SerializeField] protected List<string> Cost;
        [SerializeField] protected string Type;
        [SerializeField] protected PopUpWindow PopUpWindow;
        [SerializeField] protected PlayerDataManager _playerDataManager;
        [SerializeField] private string _englishText;
        [SerializeField] private string _turkishText;
        [SerializeField] private string _russianText;

        protected Image Background;
        protected Button Button;
        protected List<Color> Colors;
        protected string Text;
        protected int CostCountMultiplier;

        private int _indexOfCost;

        public virtual event Action<string, string> Clicked;

        public string TranslatedText { get; private set; }
        public string Class { get; protected set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
            _playerDataManager.DataReceived += SetIndex;
        }

        private void Start()
        {
            if(Text != MONEY)
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
                case English:
                    TranslatedText = _englishText;
                    break;
                case Turkish:
                    TranslatedText = _turkishText;
                    break;
                case Russian:
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

            _playerDataManager.Set(Text, _indexOfCost);
        }

        public virtual void OnClick()
        {
            Clicked?.Invoke(Cost[_indexOfCost], Type);
        }

        protected virtual void SetIndex(PlayerData playerData)
        {
            switch (Text)
            {
                case BODY_ARMOR:
                    _indexOfCost = playerData.BodyArmorIndex;
                    break;
                case BOOTS:
                    _indexOfCost = playerData.BootsIndex;
                    break;
                case HELMET:
                    _indexOfCost = playerData.HelmetIndex;
                    break;
                case MELLE_WEAPON_DAMAGE:
                    _indexOfCost = playerData.MelleWeaponDamageIndex;
                    break;
                case MELLE_WEAPON_RELOADING:
                    _indexOfCost = playerData.MelleWeaponReloadingIndex;
                    break;
                case RANGE_WEAPON_DAMAGE:
                    _indexOfCost = playerData.RangeWeaponDamageIndex;
                    break;
                case RANGE_WEAPON_RELOADING:
                    _indexOfCost = playerData.RangeWeaponReloadingIndex;
                    break;
                case THROWING_WEAPON_DAMAGE:
                    _indexOfCost = playerData.ThrowingWeaponDamageIndex;
                    break;
                case THROWING_WEAPON_RELOADING:
                    _indexOfCost = playerData.ThrowingWeaponReloadingIndex;
                    break;
            }

            SetCost();
        }
    }
}