public class ExperienceSystemPresenter
{
    private LevelSystem _levelSystem;
    private ZombieDied _die;
    private Experience _experience;

    public ExperienceSystemPresenter(LevelSystem levelSystem, ZombieDied die, Experience experience)
    {
        _levelSystem = levelSystem;
        _die = die;
        _experience = experience;
    }

    public void Enable()
    {
        _die.OnDied += OnDied;
    }

    public void Disable()
    {
        _die.OnDied -= OnDied;
    }

    private void OnDied()
    {
        _levelSystem.SetExperience(_experience.Count);
    }
}
