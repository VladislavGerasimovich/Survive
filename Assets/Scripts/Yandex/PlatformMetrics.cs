using Agava.YandexGames;
using UnityEngine;

namespace YandexElements
{
    public class PlatformMetrics : MonoBehaviour
    {
        public void OnCallGameReadyButtonClick()
        {
            YandexGamesSdk.GameReady();
        }
    }
}