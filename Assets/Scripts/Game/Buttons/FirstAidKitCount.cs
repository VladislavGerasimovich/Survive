using UnityEngine.UI;
using Storage;

namespace Game.Buttons
{
    public class FirstAidKitCount : CountOfExpendableItem
    {
        private const string FIRST_AID = "FIRST_AID";

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = FIRST_AID;
        }

        protected override void SetCount(PlayerData playerData)
        {
            _count = playerData.FirstAidCount;

            base.SetCount(playerData);
        }
    }
}