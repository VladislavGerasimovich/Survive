using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerBlink))]
public class PlayerMortality : MonoBehaviour
{
    [SerializeField] private TimeOfAction _timeOfAction;
    [SerializeField] private float _duration;

    private PlayerBlink _playerBlink;
    private bool _canUse;

    public event Action IsMortal;

    private void Awake()
    {
        _canUse = true;
        _playerBlink = GetComponent<PlayerBlink>();
    }

    public void AllowUse()
    {
        _canUse = true;
    }

    public void ProhibitUse()
    {
        _canUse = false;
    }

    public void RunImmortalityCoroutine()
    {
        StartCoroutine(Immortality());
    }

    private IEnumerator Immortality()
    {
        _playerBlink.StartBlinkCoroutine();
        _timeOfAction.StartRunCoroutine(_duration);
        float duration = _duration;

        while (duration >= 0)
        {
            if (_canUse == true)
            {
                duration -= Time.fixedDeltaTime;
            }

            yield return null;
        }

        _playerBlink.StopBlinkCoroutine();
        IsMortal.Invoke();
    }
}
