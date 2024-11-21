using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class Boots : Item
    {
        private const string BOOTS = "BOOTS";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(BOOTS, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.BootsIndex;

            base.SetIndex(playerData);
        }
    }
}