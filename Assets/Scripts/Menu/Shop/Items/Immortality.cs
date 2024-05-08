using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immortality : ConsumableItem
{
    private const string IMMORTALITY = "IMMORTALITY";

    private void Start()
    {
        _currentCount = PlayerPrefs.GetInt(IMMORTALITY);

        if (_currentCount >= _maxCount)
        {
            _button.interactable = false;
        }

        SetTextOfCount();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(IMMORTALITY, _currentCount);
        PlayerPrefs.Save();
    }
}
