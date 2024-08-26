using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementCard : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _info;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _containerOfLevels;
    [SerializeField] private Image _levelImage;
    [SerializeField] private Sprite _levelIcon;
    [SerializeField] private Color _targetColor;

    private Transform _mainContainer;
    private List<Image> _iconsColor;
    private int _currentLevel;

    public event Action<string> Click;

    public string Type {  get; private set; }
    public bool CanUse {  get; private set; }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnCardClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnCardClick);
    }

    private void Awake()
    {
        _iconsColor = new List<Image>();
    }

    public void Render(Sprite icon, string text, int countOfLevels, string type, bool canUse, Transform mainContainer)
    {
        for (int i = 0; i < countOfLevels; i++)
        {
            Image levelImage = Instantiate(_levelImage, _containerOfLevels.transform);
            levelImage.sprite = _levelIcon;
            _iconsColor.Add(levelImage);
        }

        _image.sprite = icon;
        _info.text = text;
        Type = type;
        CanUse = canUse;
        _mainContainer = mainContainer;
    }

    public void ProhibitUse()
    {
        CanUse = false;
    }

    public void Allowuse()
    {
        CanUse = true;
    }

    public void SetParent()
    {
        transform.SetParent(_mainContainer);
    }

    public void EnableButton()
    {
        _button.interactable = true;
    }

    public void DisableButton()
    {
        _button.interactable = false;
    }

    private void OnCardClick()
    {
        _button.interactable = false;
        Click?.Invoke(Type);

        if(_iconsColor.Count > 0)
        {
            _iconsColor[_currentLevel].color = _targetColor;
            _currentLevel++;
        }
    }
}
