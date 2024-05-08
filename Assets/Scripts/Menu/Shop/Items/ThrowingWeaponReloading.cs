using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeaponReloading : Item
{
    private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(THROWING_WEAPON_RELOADING);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(THROWING_WEAPON_RELOADING, _indexOfCost);
        PlayerPrefs.Save();
    }
}
