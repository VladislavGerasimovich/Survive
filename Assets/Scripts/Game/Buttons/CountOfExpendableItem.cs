using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Buttons
{
    [RequireComponent(typeof(Image))]
    public class CountOfExpendableItem : MonoBehaviour
    {
        private const string FIRST_AID = "FIRST_AID";
        private const string IMMORTALITY = "IMMORTALITY";

        [SerializeField] private PlayerDataManager _playerDataManager;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _spriteForReward;

        protected int Count;
        protected Image Image;
        protected string Text;

        private void OnEnable()
        {
            _playerDataManager.DataReceived += SetCount;
        }

        private void OnDisable()
        {
            _playerDataManager.DataReceived -= SetCount;
        }

        public void ReduceCount()
        {
            if (Count > 0)
            {
                Count--;
                _playerDataManager.Set(Text, Count);
                _infoText.text = Count.ToString();

                if (Count <= 0)
                {
                    Image.sprite = _spriteForReward;
                    _infoText.enabled = false;
                }
            }
        }

        public bool IsCountGreaterThenZero()
        {
            return Count > 0;
        }

        private void SetCount(PlayerData playerData)
        {
            switch (Text)
            {
                case FIRST_AID:
                    Count = playerData.FirstAidCount;
                    break;
                case IMMORTALITY:
                    Count = playerData.ImmortalityCount;
                    break;
                default:
                    break;
            }

            SetUserInterface();
        }

        private void SetUserInterface()
        {
            _infoText.text = Count.ToString();

            if (Count <= 0)
            {
                _infoText.enabled = false;
            }

            if (Count > 0)
            {
                Image.sprite = _normalSprite;
                return;
            }

            Image.sprite = _spriteForReward;
        }
    }
}