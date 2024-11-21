using System;
using Agava.YandexGames;
using UnityEngine;

namespace Storage
{
    public class PlayerDataManager : MonoBehaviour
    {
        private const string Money = "MONEY";
        private const string HelmetIndex = "HELMET";
        private const string BodyArmorIndex = "BODY_ARMOR";
        private const string BootsIndex = "BOOTS";
        private const string MelleWeaponDamageIndex = "MELLE_WEAPON_DAMAGE";
        private const string MelleWeaponReloadingIndex = "MELLE_WEAPON_RELOADING";
        private const string RangeWeaponDamageIndex = "RANGE_WEAPON_DAMAGE";
        private const string RangeWeaponReloadingIndex = "RANGE_WEAPON_RELOADING";
        private const string ThrowingWeaponDamageIndex = "THROWING_WEAPON_DAMAGE";
        private const string ThrowingWeaponReloadingIndex = "THROWING_WEAPON_RELOADING";
        private const string FirstAidCount = "FIRST_AID";
        private const string ImmortalityCount = "IMMORTALITY";

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
                case HelmetIndex:
                    _playerData.HelmetIndex = value;
                    break;
                case BodyArmorIndex:
                    _playerData.BodyArmorIndex = value;
                    break;
                case BootsIndex:
                    _playerData.BootsIndex = value;
                    break;
                case MelleWeaponDamageIndex:
                    _playerData.MelleWeaponDamageIndex = value;
                    break;
                case MelleWeaponReloadingIndex:
                    _playerData.MelleWeaponReloadingIndex = value;
                    break;
                case RangeWeaponDamageIndex:
                    _playerData.RangeWeaponDamageIndex = value;
                    break;
                case RangeWeaponReloadingIndex:
                    _playerData.RangeWeaponReloadingIndex = value;
                    break;
                case ThrowingWeaponDamageIndex:
                    _playerData.ThrowingWeaponDamageIndex = value;
                    break;
                case ThrowingWeaponReloadingIndex:
                    _playerData.ThrowingWeaponReloadingIndex = value;
                    break;
                case FirstAidCount:
                    _playerData.FirstAidCount = value;
                    break;
                case ImmortalityCount:
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
            _playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
            DataReceived?.Invoke(_playerData);
        }

        private void Save(PlayerData playerData)
        {
            string jsonString = JsonUtility.ToJson(playerData);
            PlayerAccount.SetCloudSaveData(jsonString);
        }
    }
}