using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private PlayerDataManager _playerDataManager;

    private const string REWARD = "CURRENTREWARD";
    private const string MONEY = "MONEY";

    private PlayerData _playerData;
    private int _normalLevelReward;
    private bool _isNormalLevelPassed;

    public int ExtraLevelReward { get; private set; }
    public int AllRewards { get; private set; }

    private void Awake()
    {
        _normalLevelReward = PlayerPrefs.GetInt(REWARD, 0);
        ExtraLevelReward = _normalLevelReward / 4;
        Debug.Log(_normalLevelReward + " normallevelreward");
        Debug.Log(ExtraLevelReward + " extralevelreward");
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
        if(_isNormalLevelPassed == false)
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
        _playerData.Money += _normalLevelReward;
        _playerDataManager.Set(MONEY, _playerData.Money);
        Debug.Log(AllRewards + " normalRewards");
    }

    private void AddExtraLevelReward()
    {
        AllRewards += ExtraLevelReward;
        _playerData.Money += ExtraLevelReward;
        _playerDataManager.Set(MONEY, _playerData.Money);
        Debug.Log(AllRewards + " extraRewards");
    }

    private void SetPlayerData(PlayerData playerData)
    {
        _playerData = playerData;
    }
}
