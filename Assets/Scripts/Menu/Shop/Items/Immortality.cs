using Storage;

namespace Menu.Shop.Items
{
    public class Immortality : ConsumableItem
    {
        private const string IMMORTALITY = "IMMORTALITY";

        private void Awake()
        {
            Text = IMMORTALITY;
        }
    }
}