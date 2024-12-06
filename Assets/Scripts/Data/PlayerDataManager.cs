using System;
using Agava.YandexGames;
using UnityEngine;

namespace Storage
{
    public class PlayerDataManager : MonoBehaviour
    {
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
                case Constants.Money:
                    _playerData.Money = value;
                    break;
                case Constants.Helmet:
                    _playerData.HelmetIndex = value;
                    break;
                case Constants.BodyArmor:
                    _playerData.BodyArmorIndex = value;
                    break;
                case Constants.Boots:
                    _playerData.BootsIndex = value;
                    break;
                case Constants.MelleWeaponDamage:
                    _playerData.MelleWeaponDamageIndex = value;
                    break;
                case Constants.MelleWeaponReloading:
                    _playerData.MelleWeaponReloadingIndex = value;
                    break;
                case Constants.RangeWeaponDamage:
                    _playerData.RangeWeaponDamageIndex = value;
                    break;
                case Constants.RangeWeaponReloading:
                    _playerData.RangeWeaponReloadingIndex = value;
                    break;
                case Constants.ThrowingWeaponDamage:
                    _playerData.ThrowingWeaponDamageIndex = value;
                    break;
                case Constants.ThrowingWeaponReloading:
                    _playerData.ThrowingWeaponReloadingIndex = value;
                    break;
                case Constants.FirstAid:
                    _playerData.FirstAidCount = value;
                    break;
                case Constants.Immortality:
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