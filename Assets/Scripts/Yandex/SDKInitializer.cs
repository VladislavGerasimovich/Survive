using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlatformMetrics))]
public sealed class SDKInitializer : MonoBehaviour
{
    [SerializeField] private int _menuSceneId;

    private PlatformMetrics _platformMetrics;

    private void Awake()
    {
        _platformMetrics = GetComponent<PlatformMetrics>();
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        _platformMetrics.OnCallGameReadyButtonClick();
        SceneManager.LoadScene(_menuSceneId);
    }
}
