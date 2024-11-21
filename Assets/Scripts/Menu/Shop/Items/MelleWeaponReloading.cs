using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class MelleWeaponReloading : Item
    {
        private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(MELLE_WEAPON_RELOADING, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.MelleWeaponReloadingIndex;

            base.SetIndex(playerData);
        }
    }
}