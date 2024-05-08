using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeaponDamage : Item
{
    private const string THROWING_WEAPON_DAMAGE = "THROWING_WEAPON_DAMAGE";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(THROWING_WEAPON_DAMAGE);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(THROWING_WEAPON_DAMAGE, _indexOfCost);
        PlayerPrefs.Save();
    }
}
