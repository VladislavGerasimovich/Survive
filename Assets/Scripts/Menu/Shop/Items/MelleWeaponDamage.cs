using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class MelleWeaponDamage : Item
    {
        private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(MELLE_WEAPON_DAMAGE, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.MelleWeaponDamageIndex;

            base.SetIndex(playerData);
        }
    }
}