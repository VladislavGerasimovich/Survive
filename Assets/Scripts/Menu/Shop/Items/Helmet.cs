using UnityEngine;
using Storage;

namespace Menu.Shop.Items
{
    public class Helmet : Item
    {
        private const string HELMET = "HELMET";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(HELMET, IndexOfCost);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            IndexOfCost = playerData.HelmetIndex;

            base.SetIndex(playerData);
        }
    }
}