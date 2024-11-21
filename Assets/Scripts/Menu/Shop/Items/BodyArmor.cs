using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class BodyArmor : Item
    {
        private const string BODY_ARMOR = "BODY_ARMOR";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(BODY_ARMOR, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.BodyArmorIndex;

            base.SetIndex(playerData);
        }
    }
}