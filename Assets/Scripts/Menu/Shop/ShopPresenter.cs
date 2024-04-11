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
    
    public ShopPresenter(Shop shop, List<Item> items, TMP_Text moneyText)
    {
        _shop = shop;
        _items = items;
        _moneyText = moneyText;
    }

    public void Enable()
    {
        _moneyText.text = _shop.Money.ToString();
        _shop.ItemBuyed += SetStatusOfItem;

        foreach (Item item in _items)
        {
            item.Clicked += BuyItem;
        }
    }

    public void Disable()
    {
        _shop.ItemBuyed += SetStatusOfItem;

        foreach (Item item in _items)
        {
            item.Clicked -= BuyItem;
        }
    }

    private void BuyItem(string money, string type)
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
    }

    private void SetStatusOfItem()
    {

    }
}