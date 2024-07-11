using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class MelleWeaponReloading : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";

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

        _playerDataManager.Set(MELLE_WEAPON_RELOADING, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.MelleWeaponReloadingIndex;
        SetCost();
    }
}
