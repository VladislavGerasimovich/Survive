using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class ThrowingWeaponDamage : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string THROWING_WEAPON_DAMAGE = "THROWING_WEAPON_DAMAGE";

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

        _playerDataManager.Set(THROWING_WEAPON_DAMAGE, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.ThrowingWeaponDamageIndex;
        SetCost();
    }
}
