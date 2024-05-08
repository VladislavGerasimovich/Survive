using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleWeaponDamage : Item
{
    private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(MELLE_WEAPON_DAMAGE);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(MELLE_WEAPON_DAMAGE, _indexOfCost);
        PlayerPrefs.Save();
    }
}
