namespace Game.UI
{
    public class HealthBar : ExperienceBar
    {
        private void Awake()
        {
            OnValueChanged(1);
        }
    }
}