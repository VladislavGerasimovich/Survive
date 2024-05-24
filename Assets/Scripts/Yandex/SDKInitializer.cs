using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SDKInitializer : MonoBehaviour
{
    [SerializeField] private int _menuSceneId;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
        PlayerAccount.GetCloudSaveData();
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(_menuSceneId);
    }
}
