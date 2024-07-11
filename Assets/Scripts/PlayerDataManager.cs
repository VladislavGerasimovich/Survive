using Agava.YandexGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Money;
    public int HelmetIndex;
    public int BodyArmorIndex;
    public int BootsIndex;
    public int MelleWeaponDamageIndex;
    public int MelleWeaponReloadingIndex;
    public int RangeWeaponDamageIndex;
    public int RangeWeaponReloadingIndex;
    public int ThrowingWeaponDamageIndex;
    public int ThrowingWeaponReloadingIndex;
    public int FirstAidCount;
    public int ImmortalityCount;
}

public class PlayerDataManager : MonoBehaviour
{
    private const string Money = "MONEY";
    private const string Helmet = "HELMET";
    private const string BODY_ARMOR = "BODY_ARMOR";
    private const string BOOTS = "BOOTS";
    private const string MELLE_WEAPON_DAMAGE = "MELLE_WEAPON_DAMAGE";
    private const string MELLE_WEAPON_RELOADING = "MELLE_WEAPON_RELOADING";
    private const string RANGE_WEAPON_DAMAGE = "RANGE_WEAPON_DAMAGE";
    private const string RANGE_WEAPON_RELOADING = "RANGE_WEAPON_RELOADING";
    private const string THROWING_WEAPON_DAMAGE = "THROWING_WEAPON_DAMAGE";
    private const string THROWING_WEAPON_RELOADING = "THROWING_WEAPON_RELOADING";
    private const string FIRST_AID = "FIRST_AID";
    private const string IMMORTALITY = "IMMORTALITY";

    private PlayerData _playerData;

    public event Action<PlayerData> DataReceived;

    private void Awake()
    {
        _playerData = new PlayerData();
        GetCloudSaveData();
    }

    public void Set(string key, int value)
    {
        switch (key)
        {
            case Money:
                _playerData.Money = value;
                break;
            case Helmet:
                _playerData.HelmetIndex = value;
                break;
            case BODY_ARMOR:
                _playerData.BodyArmorIndex = value;
                break;
            case BOOTS:
                _playerData.BootsIndex = value;
                break;
            case MELLE_WEAPON_DAMAGE:
                _playerData.MelleWeaponDamageIndex = value;
                break;
            case MELLE_WEAPON_RELOADING:
                _playerData.MelleWeaponReloadingIndex = value;
                break;
            case RANGE_WEAPON_DAMAGE:
                _playerData.RangeWeaponDamageIndex = value;
                break;
            case RANGE_WEAPON_RELOADING:
                _playerData.RangeWeaponReloadingIndex = value;
                break;
            case THROWING_WEAPON_DAMAGE:
                _playerData.ThrowingWeaponDamageIndex = value;
                break;
            case THROWING_WEAPON_RELOADING:
                _playerData.ThrowingWeaponReloadingIndex = value;
                break;
            case FIRST_AID:
                _playerData.FirstAidCount = value;
                break;
            case IMMORTALITY:
                _playerData.ImmortalityCount = value;
                break;
        }

        Save(_playerData);
    }

    public void GetCloudSaveData()
    {
        PlayerAccount.GetCloudSaveData(Get);
    }

    private void Get(string playerDataJson)
    {
        Debug.Log("successCallback");
        _playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
        DataReceived?.Invoke(_playerData);
    }

    private void Save(PlayerData playerData)
    {
        string jsonString = JsonUtility.ToJson(playerData);
        PlayerAccount.SetCloudSaveData(jsonString);
    }
}
