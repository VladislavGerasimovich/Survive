using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class AllMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _mainMusic;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;

    private const string SOUND = "SOUND";

    private Image _image;
    private bool _isPlaying;
    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _isPlaying = PlayerPrefs.GetInt(SOUND, 1) == 1 ? true : false;
        SetIcon();
        SetVolume();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SetStatus);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SetStatus);
    }

    private void SetIcon()
    {
        if(_isPlaying == true)
        {
            _image.sprite = _soundOn;

            return;
        }
        else
        {
            _image.sprite = _soundOff;
        }
    }

    private void SetStatus()
    {
        _isPlaying = !_isPlaying;
        PlayerPrefs.SetInt(SOUND, _isPlaying ? 1 : 0);
        PlayerPrefs.Save();
        SetIcon();
        SetVolume();
    }

    private void SetVolume()
    {
        if (_isPlaying == true)
        {
            _mainMusic.enabled = true;

            return;
        }
        else
        {
            _mainMusic.enabled = false;
        }
    }
}
