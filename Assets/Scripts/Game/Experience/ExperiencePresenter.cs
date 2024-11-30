using Game.Player.Levels;
using Game.Zombie;

public class ExperiencePresenter
{
    private Level _levelSystem;
    private ZombieDead _die;
    private Experience _experience;

    public ExperiencePresenter(Level levelSystem, ZombieDead die, Experience experience)
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