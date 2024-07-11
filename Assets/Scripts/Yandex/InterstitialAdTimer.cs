using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialAdTimer : MonoBehaviour
{
    private const int MaxValue = 60;

    private float _currentValue;
    private int _numberOfAdImpressions;
    private Coroutine _RunCoroutine;

    public bool IsReached {  get; private set; }

    private void Awake()
    {
        _numberOfAdImpressions = PlayerPrefs.GetInt("NumberOfAdImpressions", 0);
        Debug.Log(_numberOfAdImpressions + " number of ad impressions");
    }

    private void Start()
    {
        _currentValue = PlayerPrefs.GetFloat("TimerValue", 0);
        Debug.Log(_currentValue + "current Timer Value in Start");

        if(_currentValue >= MaxValue)
        {
            _currentValue = 0;
        }

        if(_numberOfAdImpressions > 0)
        {
            _RunCoroutine = StartCoroutine(Run());
        }
    }

    private void OnDisable()
    {
        if(_RunCoroutine != null)
        {
            StopCoroutine(_RunCoroutine);
        }

        PlayerPrefs.SetFloat("TimerValue", _currentValue);
        PlayerPrefs.Save();
    }

    private IEnumerator Run()
    {
        while(_currentValue < MaxValue)
        {
            _currentValue += Time.deltaTime;
            //Debug.Log(_currentValue + " currentTimerValue");

            yield return null;
        }

        if(_currentValue >= MaxValue)
        {
            IsReached = true;
        }
    }
}
