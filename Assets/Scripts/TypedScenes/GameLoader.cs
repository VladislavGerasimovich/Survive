using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int GameSceneId;

    private float _divider;

    private void Awake()
    {
        _divider = 0.9f;
    }

    private void Start()
    {
        StartCoroutine(RunGame());
    }

    private IEnumerator RunGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameSceneId);

        while(operation.isDone == false)
        {
            float progress = operation.progress / _divider;
            _slider.value = progress;

            yield return null;
        }
    }
}

/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

[RequireComponent(typeof(VideoAd))]
public class GameLoader : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private int GameSceneId;

    private VideoAd _videoAd;
    private float _divider;

    private void Awake()
    {
        _divider = 0.9f;
    }

    private void OnEnable()
    {
        _videoAd.AddReward += StartRunGameCoroutine;
    }

    private void Start()
    {
        _videoAd.Show();
    }

    private void OnDisable()
    {
        _videoAd.AddReward -= StartRunGameCoroutine;
    }

    private void StartRunGameCoroutine()
    {
        StartCoroutine(RunGame());
    }

    private IEnumerator RunGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameSceneId);

        while(operation.isDone == false)
        {
            float progress = operation.progress / _divider;
            _slider.value = progress;

            yield return null;
        }
    }
}

 */
