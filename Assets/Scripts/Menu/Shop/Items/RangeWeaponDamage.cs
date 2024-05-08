using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponDamage : Item
{
    private const string RANGE_WEAPON_DAMAGE = "RANGE_WEAPON_DAMAGE";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(RANGE_WEAPON_DAMAGE);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(RANGE_WEAPON_DAMAGE, _indexOfCost);
        PlayerPrefs.Save();
    }
}
