using TMPro;
using Game.Player.Levels;
using Game.UI;
using Game.UI.Screens;
using UnityEngine;

namespace Game.ObjectPools.ZombiePools
{
    [RequireComponent(typeof(SimpleZombiePool))]
    [RequireComponent(typeof(FastZombiePool))]
    [RequireComponent(typeof(BigZombiePool))]
    public class ZombiesPools : MonoBehaviour
    {
        [SerializeField] private Bar _experienceBar;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ImprovementPanel _improvementPanel;
        [SerializeField] private GameTime _gameTime;

        private SimpleZombiePool _simpleZombiePool;
        private FastZombiePool _fastZombiePool;
        private BigZombiePool _bigZombiePool;
        private UserInterfacePresenter _userInterfacePresenter;
        private Level _levelSystem;

        private void Awake()
        {
            _levelSystem = new Level();
            _simpleZombiePool = GetComponent<SimpleZombiePool>();
            _fastZombiePool = GetComponent<FastZombiePool>();
            _bigZombiePool = GetComponent<BigZombiePool>();
            _userInterfacePresenter = new UserInterfacePresenter(
                _levelSystem,
                _experienceBar,
                _text,
                _improvementPanel,
                _gameTime);
        }

        private void OnEnable()
        {
            _userInterfacePresenter.Enable();
        }

        private void Start()
        {
            _simpleZombiePool.CreateZombies(_levelSystem);
            _fastZombiePool.CreateZombies(_levelSystem);
            _bigZombiePool.CreateZombies(_levelSystem);
        }

        private void OnDisable()
        {
            _userInterfacePresenter.Disable();
        }

        public void TryGetSimpleZombie(out GameObject zombie)
        {
            _simpleZombiePool.TryGetObject(out GameObject simpleZombie);
            zombie = simpleZombie;
        }

        public void TryGetFastZombie(out GameObject zombie)
        {
            _fastZombiePool.TryGetObject(out GameObject fastZombie);
            zombie = fastZombie;
        }

        public void TryGetBigZombie(out GameObject zombie)
        {
            _bigZombiePool.TryGetObject(out GameObject bigZombie);
            zombie = bigZombie;
        }

        public void StopSound()
        {
            _simpleZombiePool.StopSound();
            _fastZombiePool.StopSound();
            _bigZombiePool.StopSound();
        }

        public void PauseSound()
        {
            _simpleZombiePool.PauseSound();
            _fastZombiePool.PauseSound();
            _bigZombiePool.PauseSound();
        }

        public void PlaySound()
        {
            _simpleZombiePool.PlaySound();
            _fastZombiePool.PlaySound();
            _bigZombiePool.PlaySound();
        }

        public void DisableSound()
        {
            _simpleZombiePool.DisableSound();
            _fastZombiePool.DisableSound();
            _bigZombiePool.DisableSound();
        }

        public void EnableSound()
        {
            _simpleZombiePool.EnableSound();
            _fastZombiePool.EnableSound();
            _bigZombiePool.EnableSound();
        }
    }
}