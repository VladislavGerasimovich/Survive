public class CharacterHealthSystemPresenter
{
    private HealthSystem _healthSystem;
    private CollisionHandler _collisionHandler;
    private Die _characterDied;
    private EnemyBlink _enemyBlink;

    public CharacterHealthSystemPresenter(HealthSystem model, CollisionHandler collisionHandler, Die die, EnemyBlink enemyBlink)
    {
        _healthSystem = model;
        _collisionHandler = collisionHandler;
        _characterDied = die;
        _enemyBlink = enemyBlink;
    }

    public void Enable()
    {
        _healthSystem.Died += OnDied;
        _collisionHandler.Collided += OnCollided;
    }

    public void Disable()
    {
        _healthSystem.Died -= OnDied;
        _collisionHandler.Collided -= OnCollided;
    }

    private void OnCollided(int damage)
    {
        _healthSystem.TakeDamage(damage);
        _enemyBlink.StartBlinkCoroutine();
    }

    private void OnDied()
    {
        _characterDied.Died();
    }
}
