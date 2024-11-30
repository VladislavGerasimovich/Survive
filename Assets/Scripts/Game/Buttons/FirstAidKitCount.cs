using Storage;
using UnityEngine.UI;

namespace Game.Buttons
{
    public class FirstAidKitCount : CountOfExpendableItem
    {
        private const string FIRST_AID = "FIRST_AID";

        private void Awake()
        {
            Image = GetComponent<Image>();
            Text = FIRST_AID;
        }
    }
}