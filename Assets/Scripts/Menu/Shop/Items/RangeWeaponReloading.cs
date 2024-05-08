using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponReloading : Item
{
    private const string RANGE_WEAPON_RELOADING = "RANGE_WEAPON_RELOADING";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(RANGE_WEAPON_RELOADING);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(RANGE_WEAPON_RELOADING, _indexOfCost);
        PlayerPrefs.Save();
    }
}
