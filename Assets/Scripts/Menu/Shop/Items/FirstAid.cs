using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : ConsumableItem
{
    private const string FIRST_AID = "FIRST_AID";

    private void Start()
    {
        _currentCount = PlayerPrefs.GetInt(FIRST_AID);

        if (_currentCount >= _maxCount)
        {
            _button.interactable = false;
        }

        SetTextOfCount();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(FIRST_AID, _currentCount);
        PlayerPrefs.Save();
    }
}
