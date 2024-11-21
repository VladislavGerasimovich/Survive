using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Storage;
using Menu.Shop.Items;

namespace Menu.Shop
{
    public class ShopPresenter : MonoBehaviour
    {
        private Wallet _shop;
        private List<Item> _items;
        private TMP_Text _moneyText;
        private string _moneyType;
        private PlayerDataManager _playerDataManager;
        private PopUpWindow _popUpWindow;
        private int _itemCost;
        private string _itemType;

        public ShopPresenter(Wallet shop, List<Item> items, TMP_Text moneyText, PlayerDataManager playerDataManager, PopUpWindow popUpWindow)
        {
            _shop = shop;
            _items = items;
            _moneyText = moneyText;
            _moneyType = "MONEY";
            _playerDataManager = playerDataManager;
            _popUpWindow = popUpWindow;
        }

        public void Enable()
        {
            foreach (Item item in _items)
            {
                item.Clicked += OnItemClicked;
            }

            _playerDataManager.DataReceived += InitializeShop;
        }

        public void Disable()
        {
            foreach (Item item in _items)
            {
                item.Clicked -= OnItemClicked;
            }

            _playerDataManager.DataReceived -= InitializeShop;
            _popUpWindow.YesButtonClicked -= BuyItem;
            _popUpWindow.NoButtonClicked -= Cancel;
        }

        private void InitializeShop(PlayerData playerData)
        {
            _shop.Initialize(playerData.Money);
            _moneyText.text = _shop.Money.ToString();
        }

        private void OnItemClicked(string money, string type)
        {
            if (type != _moneyType)
            {
                if (_shop.TrySpentMoney(int.Parse(money)))
                {
                    foreach (Item item in _items)
                    {
                        if (item.Class == type)
                        {
                            _itemCost = int.Parse(money);
                            _itemType = type;
                            _popUpWindow.Open(item.TranslatedText);
                            _popUpWindow.YesButtonClicked += BuyItem;
                            _popUpWindow.NoButtonClicked += Cancel;
                        }
                    }
                }

                return;
            }
            else
            {
                _shop.AddMoney(int.Parse(money));
                _playerDataManager.Set(_moneyType, _shop.Money);
                _moneyText.text = _shop.Money.ToString();
            }
        }

        private void BuyItem()
        {
            _shop.SpentMoney(_itemCost);
            _playerDataManager.Set(_moneyType, _shop.Money);
            _moneyText.text = _shop.Money.ToString();

            foreach (Item item in _items)
            {
                if (item.Class == _itemType)
                {
                    item.SetStatus();
                }
            }

            _itemCost = 0;
            _itemType = string.Empty;
            _popUpWindow.YesButtonClicked -= BuyItem;
            _popUpWindow.NoButtonClicked -= Cancel;
        }

        private void Cancel()
        {
            _popUpWindow.Close();
            _itemCost = 0;
            _itemType = string.Empty;
            _popUpWindow.YesButtonClicked -= BuyItem;
            _popUpWindow.NoButtonClicked -= Cancel;
        }
    }
}