using UnityEngine;

public class MelleWeaponReloading : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";

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

        _playerDataManager.Set(MELLE_WEAPON_RELOADING, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.MelleWeaponReloadingIndex;
        SetCost();
    }
}
