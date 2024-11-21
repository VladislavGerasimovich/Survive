using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class RangeWeaponReloading : Item
    {
        private const string RANGE_WEAPON_RELOADING = "RANGE_WEAPON_RELOADING";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(RANGE_WEAPON_RELOADING, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.RangeWeaponReloadingIndex;

            base.SetIndex(playerData);
        }
    }
}