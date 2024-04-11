using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<string> _cost;
    [SerializeField] private string _type;

    private Image _background;
    private Button _button;
    private int _indexOfCost;
    private List<Color> _colors;

    public event Action<string, string> Clicked;

    public string Type { get; private set; }

    private void Awake()
    {
        _colors = new List<Color>
        {
            new Color32(125, 120, 126, 255),
            new Color32(170, 238, 147, 255),
            new Color32(244, 105, 255, 255)
        };

        _background = GetComponent<Image>();
        _button = GetComponent<Button>();
        Type = _type;
    }

    private void Start()
    {
        SetCost();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void SetCost()
    {
        _text.text = _cost[_indexOfCost];
        _background.color = _colors[_indexOfCost];
        Debug.Log(_colors[_indexOfCost]);
        Debug.Log(_background.color + " back");
    }

    public void SetStatus()
    {
        if(_indexOfCost < _cost.Count)
        {
            _indexOfCost++;

            if (_indexOfCost != _cost.Count)
            {
                SetCost();
            }
        }
    }

    private void OnClick()
    {
        if (_indexOfCost < _cost.Count - 1)
        {
            Clicked?.Invoke(_cost[_indexOfCost], _type);
        }
    }
}
