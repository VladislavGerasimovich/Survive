using UnityEngine;

public class Helmet : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string HELMET = "HELMET";

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

        _playerDataManager.Set(HELMET, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.HelmetIndex;
        SetCost();
    }
}
