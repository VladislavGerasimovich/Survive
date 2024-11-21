using Storage;

namespace Menu.Shop.Items
{
    public class Immortality : ConsumableItem
    {
        private const string IMMORTALITY = "IMMORTALITY";

        public override void SetStatus()
        {
            base.SetStatus();

            _playerDataManager.Set(IMMORTALITY, CurrentCount);
        }

        protected override void SetIndex(PlayerData playerData)
        {
            CurrentCount = playerData.ImmortalityCount;

            base.SetIndex(playerData);
        }
    }
}