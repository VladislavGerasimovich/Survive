using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : ConsumableItem
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string FIRST_AID = "FIRST_AID";

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

        _playerDataManager.Set(FIRST_AID, _currentCount);
    }

    private void SetIndex(PlayerData playerData)
    {
        _currentCount = playerData.FirstAidCount;

        if (_currentCount >= _maxCount)
        {
            _button.interactable = false;
        }

        SetTextOfCount();
    }
}
