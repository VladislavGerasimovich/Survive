using UnityEngine;

public class ThrowingWeaponReloading : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";

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

        _playerDataManager.Set(THROWING_WEAPON_RELOADING, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.ThrowingWeaponReloadingIndex;
        SetCost();
    }
}
