using UnityEngine;

public class MelleWeaponDamage : Item
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";

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

        _playerDataManager.Set(MELLE_WEAPON_DAMAGE, IndexOfCost);
    }

    private void SetIndex(PlayerData playerData)
    {
        IndexOfCost = playerData.MelleWeaponDamageIndex;
        SetCost();
    }
}
