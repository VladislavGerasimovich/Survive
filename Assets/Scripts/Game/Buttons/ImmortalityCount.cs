using UnityEngine.UI;
using Storage;

namespace Game.Buttons
{
    public class ImmortalityCount : CountOfExpendableItem
    {
        private const string IMMORTALITY = "IMMORTALITY";

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = IMMORTALITY;
        }

        protected override void SetCount(PlayerData playerData)
        {
            _count = playerData.ImmortalityCount;

            base.SetCount(playerData);
        }
    }
}