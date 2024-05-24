using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class MelleWeaponReloading : Item
{
    private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";

    private void Start()
    {
        _indexOfCost = PlayerPrefs.GetInt(MELLE_WEAPON_RELOADING);
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(MELLE_WEAPON_RELOADING, _indexOfCost);
        PlayerPrefs.Save();
    }
}
