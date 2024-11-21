using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class ThrowingWeaponDamage : Item
    {
        private const string THROWING_WEAPON_DAMAGE = "THROWING_WEAPON_DAMAGE";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(THROWING_WEAPON_DAMAGE, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.ThrowingWeaponDamageIndex;

            base.SetIndex(playerData);
        }
    }
}