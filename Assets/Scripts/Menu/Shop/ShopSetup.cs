using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSetup : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private int _money;

    private ShopPresenter _shopPresenter;

    private void Awake()
    {
        Shop shop = new Shop(_money);
        _shopPresenter = new ShopPresenter(shop, _items, _moneyText);
    }

    private void OnEnable()
    {
        _shopPresenter.Enable();
    }

    private void OnDisable()
    {
        _shopPresenter.Disable();
    }
}
