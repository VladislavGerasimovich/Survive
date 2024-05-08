using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

[RequireComponent(typeof(Image))]
public class ImmortalityCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _spriteForReward;

    private const string IMMORTALITY = "IMMORTALITY";

    private int _count;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _count = PlayerPrefs.GetInt(IMMORTALITY);
    }

    private void Start()
    {
        _text.text = _count.ToString();

        if (_count > 0)
        {
            _image.sprite = _normalSprite;
            return;
        }

        _image.sprite = _spriteForReward;
    }

    public void ReduceCount()
    {
        if(_count > 0)
        {
            _count--;
            PlayerPrefs.SetInt(IMMORTALITY, _count);
            PlayerPrefs.Save();
            _text.text = _count.ToString();

            if(_count <= 0)
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
