using Storage;

namespace Menu.Shop.Items
{
    public class FirstAid : ConsumableItem
    {
        private const string FIRST_AID = "FIRST_AID";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(FIRST_AID, CurrentCount);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            CurrentCount = playerData.FirstAidCount;

            base.SetIndex(playerData);
        }
    }
}