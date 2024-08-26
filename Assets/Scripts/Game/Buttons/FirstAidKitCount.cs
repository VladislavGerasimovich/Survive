using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PressButton))]
[RequireComponent(typeof(Image))]
public class FirstAidKitCount : MonoBehaviour
{
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _spriteForReward;

    private const string FIRST_AID = "FIRST_AID";

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
        if (_count > 0)
        {
            _count--;
            _playerDataManager.Set(FIRST_AID, _count);
            _text.text = _count.ToString();

            if (_count <= 0)
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
        _count = playerData.FirstAidCount;

        _text.text = _count.ToString();

        if(_count <= 0)
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
