using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ShopPresenter
{
    private Shop _shop;
    private List<Item> _items;
    private TMP_Text _moneyText;
    private string _moneyType;
    
    public ShopPresenter(Shop shop, List<Item> items, TMP_Text moneyText)
    {
        _shop = shop;
        _items = items;
        _moneyText = moneyText;
        _moneyType = "money";
    }

    public void Enable()
    {
        _moneyText.text = _shop.Money.ToString();

        foreach (Item item in _items)
        {
            item.Clicked += BuyItem;
        }
    }

    public void Disable()
    {
        foreach (Item item in _items)
        {
            item.Clicked -= BuyItem;
        }
    }

    private void BuyItem(string money, string type)
    {
        if(type != _moneyType)
        {
            if (_shop.TrySpentMoney(int.Parse(money)))
            {
                _moneyText.text = _shop.Money.ToString();

                foreach (Item item in _items)
                {
                    if(item.Type == type)
                    {
                        item.SetStatus();
                    }
                }
            }

            return;
        }
        else
        {
            _shop.AddMoney(int.Parse(money));
            _moneyText.text = _shop.Money.ToString();
        }
    }
}