using UnityEngine;

public class Boots : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string BOOTS = "BOOTS";

    private void OnEnable()
    {
        Button.onClick.AddListener(OnClick);
        _playerDataManager.DataReceived += SetIndex;
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnClick);
        _playerDataManager.DataReceived -= SetIndex;
    }

    public override void SetStatus()
    {
        base.SetStatus();

        _playerDataManager.Set(BOOTS, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.BootsIndex;
        SetCost();
    }
}