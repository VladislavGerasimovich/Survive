using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArmor : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string BODY_ARMOR = "BODY_ARMOR";

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

        _playerDataManager.Set(BODY_ARMOR, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.BodyArmorIndex;
        SetCost();
    }
}
