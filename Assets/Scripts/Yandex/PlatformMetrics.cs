using Agava.YandexGames;
using UnityEngine;

public class PlatformMetrics : MonoBehaviour
{
    public void OnCallGameReadyButtonClick()
    {
        YandexGamesSdk.GameReady();
    }
}
