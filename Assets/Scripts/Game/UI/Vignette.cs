using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Vignette : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Coroutine _showVignetteCoroutine;

    private void Awake()
    {
        _image.DOFade(0, 0);
    }

    public void StartShowVignetteCoroutine()
    {
        if (_showVignetteCoroutine == null)
        {
            _showVignetteCoroutine = StartCoroutine(Show());
        }
    }

    private IEnumerator Show()
    {
        _image.DOFade(0.5f, 0.3f).SetLink(gameObject);

        yield return new WaitForSeconds(0.3f);

        _image.DOFade(0, 0.3f).SetLink(gameObject);

        yield return new WaitForSeconds(0.3f);

        _showVignetteCoroutine = null;
    }
}
