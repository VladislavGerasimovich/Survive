using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBlink))]
public class PlayerMortality : MonoBehaviour
{
    private PlayerBlink _playerBlink;
    private WaitForSeconds _durationOfImmortality;

    public event Action IsMortal;

    private void Awake()
    {
        _playerBlink = GetComponent<PlayerBlink>();
        _durationOfImmortality = new WaitForSeconds(5);
    }

    public void RunImmortalityCoroutine()
    {
        StartCoroutine(Immortality());
    }

    private IEnumerator Immortality()
    {
        _playerBlink.StartBlinkCoroutine();

        yield return _durationOfImmortality;

        _playerBlink.StopBlinkCoroutine();
        IsMortal.Invoke();
    }
}
