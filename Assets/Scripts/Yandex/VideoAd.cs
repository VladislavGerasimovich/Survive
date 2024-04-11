using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VideoAd : MonoBehaviour
{
    public event UnityAction OnCloseAd;

    public void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    public void OnOpenCallback()
    {
        Time.timeScale = 0;
    }

    public void OnRewardCallback()
    {
    }

    public void OnCloseCallback()
    {
        OnCloseAd.Invoke();
        Time.timeScale = 1;
    }
}
