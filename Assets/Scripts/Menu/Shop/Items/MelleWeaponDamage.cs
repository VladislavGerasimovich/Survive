using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

public class MelleWeaponDamage : Item
{
    private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";

    private void Start()
    {
        PlayerAccount.GetCloudSaveData();
        _indexOfCost = PlayerPrefs.GetInt(MELLE_WEAPON_DAMAGE);
        Debug.Log(PlayerPrefs.HasKey(MELLE_WEAPON_DAMAGE) + " есть ли ключ урона ближнего боя");
        SetCost();
    }

    public override void SetStatus()
    {
        base.SetStatus();

        PlayerPrefs.SetInt(MELLE_WEAPON_DAMAGE, _indexOfCost);
        PlayerPrefs.Save();
    }
}
