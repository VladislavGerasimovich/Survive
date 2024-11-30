using Game.Player.Levels;
using Game.UI.Screens;
using TMPro;

namespace Game.UI
{
    public class UserInterfacePresenter
    {
        private Bar _experienceBar;
        private TMP_Text _text;
        private ImprovementPanel _improvementPanel;
        private GameTime _gameTime;
        private Level _levelSystem;

        public UserInterfacePresenter(
            Level levelSystem,
            Bar experienceBar,
            TMP_Text text,
            ImprovementPanel improvementPanel,
            GameTime gameTime)
        {
            _levelSystem = levelSystem;
            _experienceBar = experienceBar;
            _text = text;
            _improvementPanel = improvementPanel;
            _gameTime = gameTime;
        }

        public void Enable()
        {
            _levelSystem.CurrentValueExperienceChange += OnValueExperienceChanged;
            _levelSystem.LevelValueChange += OnLevelValueChange;
        }

        public void Disable()
        {
            _levelSystem.CurrentValueExperienceChange -= OnValueExperienceChanged;
            _levelSystem.LevelValueChange -= OnLevelValueChange;
        }

        private void OnLevelValueChange(int level)
        {
            _text.text = level.ToString();
            _gameTime.Stop();
            _improvementPanel.Open();
        }

        private void OnValueExperienceChanged(float value)
        {
            _experienceBar.OnValueChanged(value);
        }
    }
}