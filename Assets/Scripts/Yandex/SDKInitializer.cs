using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YandexElements
{
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
}