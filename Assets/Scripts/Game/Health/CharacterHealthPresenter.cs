using Game.Collision;
using Game.DeathOfAllCharacters;
using Game.UI;

namespace Game.Health
{
    public class CharacterHealthPresenter
    {
        private Health _healthSystem;
        private CollisionHandler _collisionHandler;
        private Death _characterDied;
        private EnemyBlink _enemyBlink;

        public CharacterHealthPresenter(
            Health model,
            CollisionHandler collisionHandler,
            Death die,
            EnemyBlink enemyBlink)
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