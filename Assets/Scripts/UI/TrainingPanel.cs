using Agava.YandexGames;
using Game;
using UnityEngine;

namespace UI
{
    public class TrainingPanel : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";

        [SerializeField] private CanvasGroup _trainingPanelDesktop;
        [SerializeField] private CanvasGroup _trainingPanelMobile;
        [SerializeField] private GameTime _gameTime;

        private CanvasGroup _currentPanel;

        public bool IsOpen { get; private set; }

        private void Awake()
        {
            if (Application.isMobilePlatform == true)
            {
                _currentPanel = _trainingPanelMobile;
                return;
            }

            _currentPanel = _trainingPanelDesktop;
        }

        private void Start()
        {
            if (PlayerAccount.IsAuthorized == false)
            {
                Open();
                return;
            }

            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if (result == null || result.score == 0)
                {
                    Open();
                }
            });
        }

        private void Update()
        {
            if (UnityEngine.Input.anyKeyDown)
            {
                IsOpen = false;
                _currentPanel.alpha = 0;
                _currentPanel.blocksRaycasts = false;
                _gameTime.Run();
            }
        }

        private void Open()
        {
            IsOpen = true;
            _gameTime.Stop();
            _currentPanel.alpha = 1;
            _currentPanel.blocksRaycasts = true;
        }
    }
}