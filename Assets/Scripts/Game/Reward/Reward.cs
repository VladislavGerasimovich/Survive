using Storage;
using TMPro;
using UnityEngine;

namespace Game.Reward
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private PlayerDataManager _playerDataManager;
        [SerializeField] private TMP_Text _text;

        private PlayerData _playerData;
        private int _normalLevelReward;
        private bool _isNormalLevelPassed;

        public int ExtraLevelReward { get; private set; }
        public int AllRewards { get; private set; }

        private void Awake()
        {
            _normalLevelReward = PlayerPrefs.GetInt(Constants.Reward, 0);
            ExtraLevelReward = _normalLevelReward / 4;
            _text.text = AllRewards.ToString();
        }

        private void OnEnable()
        {
            _playerDataManager.DataReceived += SetPlayerData;
        }

        private void OnDisable()
        {
            _playerDataManager.DataReceived -= SetPlayerData;
        }

        public void Add()
        {
            if (_isNormalLevelPassed == false)
            {
                AddNormalLevelReward();
                _isNormalLevelPassed = true;
                return;
            }

            AddExtraLevelReward();
        }

        private void AddNormalLevelReward()
        {
            AllRewards += _normalLevelReward;
            _text.text = AllRewards.ToString();
            _playerData.Money += _normalLevelReward;
            _playerDataManager.Set(Constants.Money, _playerData.Money);
        }

        private void AddExtraLevelReward()
        {
            AllRewards += ExtraLevelReward;
            _text.text = AllRewards.ToString();
            _playerData.Money += ExtraLevelReward;
            _playerDataManager.Set(Constants.Money, _playerData.Money);
        }

        private void SetPlayerData(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}