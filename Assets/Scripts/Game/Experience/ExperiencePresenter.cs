using Game.Player.Levels;
using Game.Zombie;

public class ExperiencePresenter
{
    private Level _levelSystem;
    private ZombieDied _die;
    private Experience _experience;

    public ExperiencePresenter(Level levelSystem, ZombieDied die, Experience experience)
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