using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyArmor : Item
{
    private const string BODY_ARMOR = "BODY_ARMOR";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(BODY_ARMOR,0);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(BODY_ARMOR, _indexOfCost);
        PlayerPrefs.Save();
    }
}
