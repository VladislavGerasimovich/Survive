using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float _remainigTime;
    [SerializeField] private ContinueGamePanel _continueGamePanel;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        int minutes;
        int seconds;

        while (_remainigTime > 1)
        {
            _remainigTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(_remainigTime / 60);
            seconds = Mathf.FloorToInt(_remainigTime % 60);
            _text.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }

        _continueGamePanel.Open();
    }
}
