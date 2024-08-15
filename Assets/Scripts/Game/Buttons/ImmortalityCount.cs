using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Agava.YandexGames;

[RequireComponent(typeof(Image))]
public class ImmortalityCount : MonoBehaviour
{
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _spriteForReward;

    private const string IMMORTALITY = "IMMORTALITY";

    private int _count;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _playerDataManager.DataReceived += SetCount;
    }

    private void OnDisable()
    {
        _playerDataManager.DataReceived -= SetCount;
    }

    public void ReduceCount()
    {
        if(_count > 0)
        {
            _count--;
            _playerDataManager.Set(IMMORTALITY, _count);
            _text.text = _count.ToString();

            if(_count <= 0)
            {
                _image.sprite = _spriteForReward;
                _text.enabled = false;
            }

            return;
        }
    }

    public bool IsCountGreaterThenZero()
    {
        return _count > 0;
    }

    private void SetCount(PlayerData playerData)
    {
        _count = playerData.ImmortalityCount;

        _text.text = _count.ToString();

        if (_count <= 0)
        {
            _text.enabled = false;
        }

        if (_count > 0)
        {
            _image.sprite = _normalSprite;
            return;
        }

        _image.sprite = _spriteForReward;
    }
}
