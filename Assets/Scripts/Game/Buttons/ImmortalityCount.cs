using Storage;
using UnityEngine.UI;

namespace Game.Buttons
{
    public class ImmortalityCount : CountOfExpendableItem
    {
        private const string IMMORTALITY = "IMMORTALITY";

        private void Awake()
        {
            Image = GetComponent<Image>();
            Text = IMMORTALITY;
        }
    }
}