using Game.Collision;
using Game.UI;
using Game.DeathOfAllCharacters;

namespace Game.Health
{
    public class CharacterHealthPresenter
    {
        private HealthSystem _healthSystem;
        private CollisionHandler _collisionHandler;
        private Death _characterDied;
        private EnemyBlink _enemyBlink;

        public CharacterHealthPresenter(HealthSystem model, CollisionHandler collisionHandler, Death die, EnemyBlink enemyBlink)
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
}