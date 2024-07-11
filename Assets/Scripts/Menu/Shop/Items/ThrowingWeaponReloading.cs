using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class ThrowingWeaponReloading : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
        _playerDataManager.DataReceived += SetIndex;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
        _playerDataManager.DataReceived -= SetIndex;
    }

    public override void SetStatus()
    {
        base.SetStatus();

        _playerDataManager.Set(THROWING_WEAPON_RELOADING, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.ThrowingWeaponReloadingIndex;
        SetCost();
    }
}
