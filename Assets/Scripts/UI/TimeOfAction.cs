using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TimeOfAction : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private Image _image;
    private bool _canUse;

    private void Awake()
    {
        _canUse = true;
        _image = GetComponent<Image>();
    }

    public void StartRunCoroutine(float time)
    {
        StartCoroutine(Run(time));
    }

    public void AllowUse()
    {
        _canUse = true;
    }

    public void ProhibitUse()
    {
        _canUse = false;
    }

    private IEnumerator Run(float time)
    {
        _image.fillAmount = 0;
        _canvasGroup.alpha = 1;
        float duration = time;

        while (duration >= 0)
        {
            if(_canUse == true)
            {
                duration -= Time.fixedDeltaTime;
                _image.fillAmount += 0.35f / duration * Time.fixedDeltaTime;
            }

            yield return null;
        }

        _image.fillAmount = 0;
        _canvasGroup.alpha = 0;
    }
}
