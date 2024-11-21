using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class ThrowingWeaponReloading : Item
    {
        private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(THROWING_WEAPON_RELOADING, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.ThrowingWeaponReloadingIndex;

            base.SetIndex(playerData);
        }
    }
}