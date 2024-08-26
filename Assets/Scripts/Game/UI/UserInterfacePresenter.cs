using TMPro;

public class UserInterfacePresenter
{
    private ExperienceBar _experienceBar;
    private TMP_Text _text;
    private ImprovementPanel _improvementPanel;
    private GameTime _gameTime;
    private LevelSystem _levelSystem;

    public UserInterfacePresenter(LevelSystem levelSystem, ExperienceBar experienceBar, TMP_Text text, ImprovementPanel improvementPanel, GameTime gameTime)
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