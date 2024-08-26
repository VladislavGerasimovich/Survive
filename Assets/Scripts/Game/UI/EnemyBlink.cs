using System.Collections;
using UnityEngine;

public class EnemyBlink : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private WaitForSeconds _delay;
    private Material _currentMaterial;
    private Coroutine _blinkCoroutine;

    private void Awake()
    {
        _delay = new WaitForSeconds(0.15f);
        _currentMaterial = _skinnedMeshRenderer.material;
    }

    private void OnDisable()
    {
        _skinnedMeshRenderer.material = _currentMaterial;
    }

    public void StartBlinkCoroutine()
    {
        if(_blinkCoroutine == null && gameObject.activeSelf == true)
        {
            _blinkCoroutine = StartCoroutine(Blink());
        }
    }

    public void DisableBlink()
    {
        enabled = false;
        _blinkCoroutine = null;
    }

    public void EnableBlink()
    {
        enabled = true;
    }

    private IEnumerator Blink()
    {
        _skinnedMeshRenderer.material = _newMaterial;

        yield return _delay;

        _skinnedMeshRenderer.material = _currentMaterial;
        _blinkCoroutine = null;
    }
}
