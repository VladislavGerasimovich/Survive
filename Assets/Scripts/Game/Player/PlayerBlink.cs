using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [SerializeField] private Material _newMaterial;
    [SerializeField] private List<MeshRenderer> _meshRenderers;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

    private WaitForSeconds _delay;
    private Material[] _currentMaterials;
    private Material _currentSkinnedMaterial;
    private Coroutine _blinkCoroutine;

    private void Awake()
    {
        _delay = new WaitForSeconds(0.15f);
        _currentMaterials = new Material[_meshRenderers.Count];
        _currentSkinnedMaterial = _skinnedMeshRenderer.material;

        for (int i = 0; i < _meshRenderers.Count; i++)
        {
            _currentMaterials[i] = _meshRenderers[i].material;
        }
    }

    public void StartBlinkCoroutine()
    {
        _blinkCoroutine = StartCoroutine(Blink());
    }

    public void StopBlinkCoroutine()
    {
        StopCoroutine(_blinkCoroutine);
        SetNewMaterials();
    }

    private void SetCurrentMaterials()
    {
        Debug.Log("setcur");
        for (int i = 0; i < _meshRenderers.Count; i++)
        {
            _meshRenderers[i].material = _newMaterial;
        }

        _skinnedMeshRenderer.material = _newMaterial;
    }

    private void SetNewMaterials()
    {
        for (int i = 0; i < _meshRenderers.Count; i++)
        {
            _meshRenderers[i].material = _currentMaterials[i];
        }

        _skinnedMeshRenderer.material = _currentSkinnedMaterial;
    }

    private IEnumerator Blink()
    {
        while (enabled)
        {
            SetCurrentMaterials();

            yield return _delay;

            SetNewMaterials();

            yield return _delay;
        }
    }
}
