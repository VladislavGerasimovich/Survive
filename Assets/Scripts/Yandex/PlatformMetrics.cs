using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMetrics : MonoBehaviour
{
    private void Awake()
    {
        OnCallGameReadyButtonClick();
    }

    public void OnCallGameReadyButtonClick()
    {
        YandexGamesSdk.GameReady();
    }
}
