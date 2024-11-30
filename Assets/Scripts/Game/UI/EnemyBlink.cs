using System.Collections;
using CommonVariables;
using UnityEngine;

namespace Game.UI
{
    public class EnemyBlink : MonoBehaviour
    {
        [SerializeField] private Material _newMaterial;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

        private Variables _variables;
        private Material _currentMaterial;
        private Coroutine _blinkCoroutine;

        private void Awake()
        {
            _variables = new Variables();
            _currentMaterial = _skinnedMeshRenderer.material;
        }

        private void OnDisable()
        {
            _skinnedMeshRenderer.material = _currentMaterial;
        }

        public void StartBlinkCoroutine()
        {
            if (_blinkCoroutine == null && gameObject.activeSelf == true)
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

            yield return _variables.Delay;

            _skinnedMeshRenderer.material = _currentMaterial;
            _blinkCoroutine = null;
        }
    }
}