using Game.Buttons;
using Game.Player;
using UI;
using UnityEngine;

namespace Game.UI.Screens
{
    public class ImprovementPanel : Window
    {
        [SerializeField] private PressButton _firstAidButton;
        [SerializeField] private PressButton _immortalityButton;
        [SerializeField] private CanvasGroup _gameMenuCanvasGroup;
        [SerializeField] private CanvasGroup _endGameCanvasGroup;
        [SerializeField] private CanvasGroup _continueGamePanelCanvasGroup;
        [SerializeField] private TimeOfAction _timeOfAction;
        [SerializeField] private PlayerMortality _playerMortality;

        public override void Close()
        {
            base.Close();
            _timeOfAction.AllowUse();
            _playerMortality.AllowUse();
            _continueGamePanelCanvasGroup.blocksRaycasts = true;
            _gameMenuCanvasGroup.blocksRaycasts = true;
            _endGameCanvasGroup.blocksRaycasts = true;
            CanvasGroup.alpha = 0;
            _immortalityButton.Enable();
            _firstAidButton.Enable();

            if (_firstAidButton.Interactable == true)
            {
                _firstAidButton.InteractableOn();
            }
        }

        public override void Open()
        {
            base.Open();
            _timeOfAction.ProhibitUse();
            _playerMortality.ProhibitUse();
            _continueGamePanelCanvasGroup.blocksRaycasts = false;
            _gameMenuCanvasGroup.blocksRaycasts = false;
            _endGameCanvasGroup.blocksRaycasts = false;
            CanvasGroup.blocksRaycasts = true;
            _firstAidButton.Disable();
            _immortalityButton.Disable();
            CanvasGroup.alpha = 1;
        }
    }
}