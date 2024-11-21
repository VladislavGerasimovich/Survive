using System;
using System.Collections;
using System.Collections.Generic;
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

        public event Action Click;

        public bool Interactable { get; private set; }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            Interactable = _interactable;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
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
            _button.interactable = true;
            ChangeColor(1);
        }

        public void InteractableOff()
        {
            _button.interactable = false;
            ChangeColor(0.3f);
        }

        private void OnClick()
        {
            Click?.Invoke();
        }

        private void ChangeColor(float alphaChannelValue)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alphaChannelValue);

            if (_adImage != null)
            {
                _adImage.color = new Color(_adImage.color.r, _adImage.color.g, _adImage.color.b, alphaChannelValue);
            }
        }
    }
}