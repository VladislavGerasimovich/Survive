using UnityEngine;
using UnityEngine.UI;
using CommonVariables;

namespace Menu.Music
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Variables))]
    public class AllMenuMusic : MonoBehaviour
    {
        private const string SOUND = "SOUND";

        [SerializeField] private AudioSource _mainMusic;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;

        private Image _image;
        private Button _button;
        private Variables _variables;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _variables = GetComponent<Variables>();
            bool isPlaying = PlayerPrefs.GetInt(SOUND, 1) == 1 ? true : false;
            _variables.ChangeIsPlaying(isPlaying);
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
            if (_variables.IsPlaying == true)
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
            bool isPlaying = !_variables.IsPlaying;
            _variables.ChangeIsPlaying(isPlaying);
            PlayerPrefs.SetInt(SOUND, _variables.IsPlaying ? 1 : 0);
            PlayerPrefs.Save();
            SetIcon();
            SetVolume();
        }

        private void SetVolume()
        {
            if (_variables.IsPlaying == true)
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
}