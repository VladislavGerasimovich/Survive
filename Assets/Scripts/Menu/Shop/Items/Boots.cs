using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string BOOTS = "BOOTS";

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

        _playerDataManager.Set(BOOTS, _indexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        _indexOfCost = playerData.BootsIndex;
        SetCost();
    }
}