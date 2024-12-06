using System.Collections.Generic;
using CommonVariables;
using Game.ObjectPools;
using Game.ObjectPools.ZombiePools;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Music
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class AllGameMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMusic;
        [SerializeField] private Sprite _soundOn;
        [SerializeField] private Sprite _soundOff;
        [SerializeField] private ZombiesPools _zombiesPools;
        [SerializeField] private AudioSource _shotSound;
        [SerializeField] private AudioSource _playerHurt;
        [SerializeField] private List<AudioSource> _batsSounds;
        [SerializeField] private GrenadesCreator _grenadesCreator;

        private Image _image;
        private Button _button;
        private Variables _variables;

        public bool CanPlay { get; private set; }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _variables = new Variables();
            bool isPlaying = PlayerPrefs.GetInt(Constants.Sound, 1) == 1 ? true : false;
            _variables.ChangeIsPlaying(isPlaying);
            SetIcon();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SetStatus);
        }

        private void Start()
        {
            SetVolume();
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
            PlayerPrefs.SetInt(Constants.Sound, _variables.IsPlaying ? 1 : 0);
            PlayerPrefs.Save();
            SetIcon();
            SetVolume();
        }

        private void SetVolume()
        {
            if (_variables.IsPlaying == true)
            {
                CanPlay = true;
                _mainMusic.enabled = true;
                _zombiesPools.EnableSound();
                _shotSound.enabled = true;
                _playerHurt.enabled = true;
                _grenadesCreator.EnableSound();

                foreach (AudioSource batSound in _batsSounds)
                {
                    batSound.enabled = true;
                }

                return;
            }
            else
            {
                CanPlay = false;
                _mainMusic.enabled = false;
                _zombiesPools.DisableSound();
                _shotSound.enabled = false;
                _playerHurt.enabled = false;
                _grenadesCreator.DisableSound();

                foreach (AudioSource batSound in _batsSounds)
                {
                    batSound.enabled = false;
                }
            }
        }
    }
}