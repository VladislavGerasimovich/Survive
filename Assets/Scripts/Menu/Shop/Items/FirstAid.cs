using Storage;

namespace Menu.Shop.Items
{
    public class FirstAid : ConsumableItem
    {
        private const string FIRST_AID = "FIRST_AID";

        private void Awake()
        {
            Text = FIRST_AID;
        }
    }
}