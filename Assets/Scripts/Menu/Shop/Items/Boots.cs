using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : Item
{
    private const string BOOTS = "BOOTS";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(BOOTS);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(BOOTS, _indexOfCost);
        PlayerPrefs.Save();
    }
}