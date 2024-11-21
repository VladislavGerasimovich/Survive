using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Buttons;
using Game.Player;
using TypedScenes;
using UI;
using YandexElements;

namespace Game.UI.Screens
{
    public class EndGamePanel : Window
    {
        [SerializeField] private CanvasGroup _improvementPanelCanvasGroup;
        [SerializeField] private CanvasGroup _gameMenuPanelCanvasGroup;
        [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;
        [SerializeField] private PressButton _firstAidButton;
        [SerializeField] private PressButton _immortalityButton;
        [SerializeField] private PressButton _reliveButton;
        [SerializeField] private PressButton _menuButton;
        [SerializeField] private MenuLoader _menuLoader;
        [SerializeField] private GameTime _gameTime;
        [SerializeField] private VideoAd _videoAd;
        [SerializeField] private TMP_Text _rewardText;
        [SerializeField] private Reward.Reward _reward;
        [SerializeField] private TimeOfAction _timeOfAction;
        [SerializeField] private PlayerMortality _playerMortality;

        private bool _isRewardReceived;

        private void OnEnable()
        {
            _reliveButton.Click += OnReliveButtonClick;
            _menuButton.GetComponent<Button>().onClick.AddListener(ExitMenu);
        }

        private void OnDisable()
        {
            _videoAd.OnCloseAd -= Close;
            _reliveButton.GetComponent<Button>().onClick.RemoveListener(OnReliveButtonClick);
            _menuButton.GetComponent<Button>().onClick.RemoveListener(ExitMenu);
        }

        public override void Close()
        {
            if (_isRewardReceived == true)
            {
                base.Close();

                if (_improvementPanelCanvasGroup.alpha == 1)
                {
                    _improvementPanelCanvasGroup.blocksRaycasts = true;
                }

                if (_gameMenuPanelCanvasGroup.alpha == 1)
                {
                    _gameMenuPanelCanvasGroup.blocksRaycasts = true;
                }

                if (_continueGamePanelCanvasGroup.alpha == 1)
                {
                    _continueGamePanelCanvasGroup.blocksRaycasts = true;
                }

                _timeOfAction.AllowUse();
                _playerMortality.AllowUse();
                CanvasGroup.blocksRaycasts = false;
                _reliveButton.InteractableOff();
                _immortalityButton.Enable();
                _immortalityButton.InteractableOn();
                _menuButton.InteractableOff();
                CanvasGroup.alpha = 0;
                _gameTime.Run();
                _isRewardReceived = false;
                _firstAidButton.Enable();

                if (_firstAidButton.Interactable == true)
                {
                    _firstAidButton.InteractableOn();
                }
            }
        }

        public override void Open()
        {
            base.Open();

            if (_reliveButton.Interactable == true)
            {
                _reliveButton.InteractableOn();
            }

            _timeOfAction.ProhibitUse();
            _playerMortality.ProhibitUse();
            _videoAd.OnCloseAd += Close;
            _continueGamePanelCanvasGroup.blocksRaycasts = false;
            _improvementPanelCanvasGroup.blocksRaycasts = false;
            _gameMenuPanelCanvasGroup.blocksRaycasts = false;
            _firstAidButton.Disable();
            _firstAidButton.InteractableOff();
            _immortalityButton.Disable();
            _immortalityButton.InteractableOff();
            _menuButton.InteractableOn();
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.alpha = 1;
            _gameTime.Stop();
            _rewardText.text = _reward.AllRewards.ToString();
        }

        private void OnReliveButtonClick()
        {
            _videoAd.Show();
            _videoAd.OnRewardReceived += SetStatus;
            _reliveButton.StatusInteractableOff();
            _firstAidButton.StatusInteractableOff();
        }

        private void ExitMenu()
        {
            StopAllCoroutines();
            _menuLoader.RunInterstitialAd();
        }

        private void SetStatus()
        {
            _isRewardReceived = true;
            _videoAd.OnRewardReceived -= SetStatus;
        }
    }
}