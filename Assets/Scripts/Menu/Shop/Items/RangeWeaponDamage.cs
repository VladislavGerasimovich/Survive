using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class RangeWeaponDamage : Item
    {
        private const string RANGE_WEAPON_DAMAGE = "RANGE_WEAPON_DAMAGE";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(RANGE_WEAPON_DAMAGE, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.RangeWeaponDamageIndex;

            base.SetIndex(playerData);
        }
    }
}