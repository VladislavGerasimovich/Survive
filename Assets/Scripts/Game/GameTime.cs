using Game.Player.Movement;
using Game.UI.Screens;
using Menu.Shop;
using UnityEngine;

namespace Game
{
    public class GameTime : MonoBehaviour
    {
        [SerializeField] private MobilePlayerInput _mobilePlayerInput;
        [SerializeField] private DesktopWASDPlayerInput _desktopPlayerInput;
        [SerializeField] private ImprovementPanel _improvementPanel;
        [SerializeField] private GameMenuPanel _gameMenuPanel;
        [SerializeField] private EndGamePanel _endGamePanel;
        [SerializeField] private ContinueGamePanel _continueGamePanel;
        [SerializeField] private GameOverPanel _gameOverPanel;
        [SerializeField] private PopUpWindowForGame _immortalityPopUpWindow;
        [SerializeField] private PopUpWindowForGame _firstAidPopUpWindow;

        private Player.Movement.PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = _desktopPlayerInput;

            if (Application.isMobilePlatform == true)
            {
                _playerInput = _mobilePlayerInput;
            }

            Run();
        }

        public void Stop()
        {
            Time.timeScale = 0;
            _playerInput.DisableInput();
        }

        public void Run()
        {
            if (_improvementPanel.IsOpen == false
                && _gameMenuPanel.IsOpen == false
                && _endGamePanel.IsOpen == false
                && _continueGamePanel.IsOpen == false
                && _gameOverPanel.IsOpen == false
                && _immortalityPopUpWindow.IsOpen == false
                && _firstAidPopUpWindow.IsOpen == false)
            {
                Time.timeScale = 1;
                _playerInput.EnableInput();
            }
        }
    }
}