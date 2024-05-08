using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private const string REWARD = "CURRENTREWARD";
    private const string MONEY = "MONEY";

    private int _normalLevelReward;
    private int _extraLevelReward;
    private bool _isNormalLevelPassed;

    public int AllRewards { get; private set; }

    private void Awake()
    {
        _normalLevelReward = PlayerPrefs.GetInt(REWARD, 0);
        _extraLevelReward = _normalLevelReward / 4;
        Debug.Log(_normalLevelReward + " normallevelreward");
        Debug.Log(_extraLevelReward + " extralevelreward");
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
        int money = PlayerPrefs.GetInt(MONEY) + _normalLevelReward;
        PlayerPrefs.SetInt(MONEY, money);
        PlayerPrefs.Save();
        Debug.Log(AllRewards + " normalRewards");
    }

    private void AddExtraLevelReward()
    {
        AllRewards += _extraLevelReward;
        int money = PlayerPrefs.GetInt(MONEY) + _extraLevelReward;
        PlayerPrefs.SetInt(MONEY, money);
        PlayerPrefs.Save();
        Debug.Log(AllRewards + " extraRewards");
    }
}
