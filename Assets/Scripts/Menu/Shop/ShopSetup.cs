using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSetup : MonoBehaviour
{
    [SerializeField] private List<Item> _items;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private PopUpWindow _popUpWindow;

    private ShopPresenter _shopPresenter;

    private void Awake()
    {
        Shop shop = new Shop();
        _shopPresenter = new ShopPresenter(shop, _items, _moneyText, _playerDataManager, _popUpWindow);
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
