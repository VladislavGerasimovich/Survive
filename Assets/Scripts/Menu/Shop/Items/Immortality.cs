using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortality : ConsumableItem
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string IMMORTALITY = "IMMORTALITY";

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

        _playerDataManager.Set(IMMORTALITY, _currentCount);
    }

    private void SetIndex(PlayerData playerData)
    {
        _currentCount = playerData.ImmortalityCount;

        if (_currentCount >= _maxCount)
        {
            _button.interactable = false;
        }

        SetTextOfCount();
    }
}
