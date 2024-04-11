using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VideoAdForImprovement : MonoBehaviour
{
    [SerializeField] private GameTime _gameTime;
    public event UnityAction AddReward;

    public virtual void Show() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    private void OnOpenCallback()
    {
        Time.timeScale = 0;

        _gameTime.Stop();
    }

    private void OnRewardCallback()
    {
        AddReward.Invoke();
    }


    private void OnCloseCallback()
    {
        Time.timeScale = 0;
        _gameTime.Stop();
    }
}
