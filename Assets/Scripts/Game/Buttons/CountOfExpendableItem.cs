using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Storage;

namespace Game.Buttons
{
    [RequireComponent(typeof(Image))]
    public class CountOfExpendableItem : MonoBehaviour
    {
        [SerializeField] private PlayerDataManager _playerDataManager;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _spriteForReward;

        protected int _count;
        protected Image _image;
        protected string _text;

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
            if (_count > 0)
            {
                _count--;
                _playerDataManager.Set(_text, _count);
                _infoText.text = _count.ToString();

                if (_count <= 0)
                {
                    _image.sprite = _spriteForReward;
                    _infoText.enabled = false;
                }
            }
        }

        public bool IsCountGreaterThenZero()
        {
            return _count > 0;
        }

        protected virtual void SetCount(PlayerData playerData)
        {
            _infoText.text = _count.ToString();

            if (_count <= 0)
            {
                _infoText.enabled = false;
            }

            if (_count > 0)
            {
                _image.sprite = _normalSprite;
                return;
            }

            _image.sprite = _spriteForReward;
        }
    }
}