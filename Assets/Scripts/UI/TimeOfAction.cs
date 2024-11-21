using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using CommonVariables;

namespace UI
{
    [RequireComponent(typeof(Variables))]
    [RequireComponent(typeof(Image))]
    public class TimeOfAction : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private Image _image;
        private Variables _variables;

        private void Awake()
        {
            _variables = GetComponent<Variables>();
            _image = GetComponent<Image>();
        }

        public void StartRunCoroutine(float time)
        {
            StartCoroutine(Run(time));
        }

        public void AllowUse()
        {
            _variables.ChangeCanUse(true);
        }

        public void ProhibitUse()
        {
            _variables.ChangeCanUse(false);
        }

        private IEnumerator Run(float time)
        {
            _image.fillAmount = 0;
            _canvasGroup.alpha = 1;
            float duration = time;

            while (duration >= 0)
            {
                if (_variables.CanUse == true)
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
}