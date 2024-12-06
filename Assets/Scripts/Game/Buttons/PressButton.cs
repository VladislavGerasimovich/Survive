using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Buttons
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class PressButton : MonoBehaviour
    {
        [SerializeField] private Image _adImage;
        [SerializeField] private bool _interactable;

        private Button _button;
        private Image _image;
        private float _minAlphaChannelValue;
        private float _maxAlphaChannelValue;

        public event Action Click;

        public bool Interactable { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            Interactable = _interactable;
            _minAlphaChannelValue = 0.3f;
            _maxAlphaChannelValue = 1.0f;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Disable()
        {
            transform.gameObject.SetActive(false);
        }

        public void Enable()
        {
            transform.gameObject.SetActive(true);
        }

        public void StatusInteractableOn()
        {
            Interactable = true;
        }

        public void StatusInteractableOff()
        {
            Interactable = false;
        }

        public void InteractableOn()
        {
            SetColorAndInteractivity(true, _maxAlphaChannelValue);
        }

        public void InteractableOff()
        {
            SetColorAndInteractivity(false, _minAlphaChannelValue);
        }

        private void OnClick()
        {
            Click?.Invoke();
        }

        private void ChangeColor(float alphaChannelValue)
        {
            SetColor(_image, alphaChannelValue);

            if (_adImage != null)
            {
                SetColor(_adImage, alphaChannelValue);
            }
        }

        private void SetColor(Image image, float alphaChannelValue)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alphaChannelValue);
        }

        private void SetColorAndInteractivity(bool value, float alphaChannelValue)
        {
            _button.interactable = value;
            ChangeColor(alphaChannelValue);
        }
    }
}