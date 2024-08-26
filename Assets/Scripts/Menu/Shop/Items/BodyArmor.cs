using UnityEngine;

public class BodyArmor : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string BODY_ARMOR = "BODY_ARMOR";

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

        _playerDataManager.Set(BODY_ARMOR, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.BodyArmorIndex;
        SetCost();
    }
}
