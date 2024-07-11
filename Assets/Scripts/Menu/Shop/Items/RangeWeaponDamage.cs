using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class RangeWeaponDamage : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string RANGE_WEAPON_DAMAGE = "RANGE_WEAPON_DAMAGE";

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

        _playerDataManager.Set(RANGE_WEAPON_DAMAGE, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.RangeWeaponDamageIndex;
        SetCost();
    }
}
