using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;
using System;

public class MelleWeaponDamage : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";

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

        _playerDataManager.Set(MELLE_WEAPON_DAMAGE, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.MelleWeaponDamageIndex;
        SetCost();
    }
}
