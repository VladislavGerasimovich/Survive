using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PressButton))]
[RequireComponent(typeof(Image))]
public class FirstAidKitCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _spriteForReward;

    private const string FIRST_AID = "FIRST_AID";

    private int _count;
    private PressButton _pressButton;
    private Image _image;

    private void Awake()
    {
        _pressButton = GetComponent<PressButton>();
        _image = GetComponent<Image>();
        _count = PlayerPrefs.GetInt(FIRST_AID);
    }

    private void Start()
    {
        _text.text = _count.ToString();

        if(_count > 0)
        {
            _image.sprite = _normalSprite;
            return;
        }

        _image.sprite = _spriteForReward;
    }

    public void ReduceCount()
    {
        if (_count > 0)
        {
            _count--;
            PlayerPrefs.SetInt(FIRST_AID, _count);
            PlayerPrefs.Save();
            _text.text = _count.ToString();

            if (_count <= 0)
            {
                _image.sprite = _spriteForReward;
            }

            return;
        }
    }

    public bool IsCountGreaterThenZero()
    {
        return _count > 0;
    }
}
