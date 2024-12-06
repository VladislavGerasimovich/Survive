using System;
using System.Collections;
using Game.DeathOfAllCharacters;
using Game.Player;
using Game.UI;
using UnityEngine;

namespace Game.Zombie
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(ZombieMovement))]
    [RequireComponent(typeof(Score))]
    [RequireComponent(typeof(EnemyBlink))]
    [RequireComponent(typeof(ZombieAttack))]
    public class ZombieDeath : Death
    {
        [SerializeField] private CapsuleCollider _triggerCollider;

        private AudioSource _audioSource;
        private PlayerScore _playerScore;
        private CapsuleCollider _capsuleCollider;
        private ZombieMovement _movement;
        private Rigidbody _rigidbody;
        private Score _score;
        private EnemyBlink _enemyBlink;
        private ZombieAttack _zombieAttack;
        private AnimatorData _animatorData;
        private int _heightOfFall;
        private int _twoSecDelay;
        private int _fourSecDelay;
        private float _stoppingDistance;

        public event Action OnDied;

        public bool IsDied { get; private set; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<PlayerScore>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _movement = GetComponent<ZombieMovement>();
            Animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _score = GetComponent<Score>();
            _enemyBlink = GetComponent<EnemyBlink>();
            _zombieAttack = GetComponent<ZombieAttack>();
            _animatorData = new AnimatorData();
            _heightOfFall = -2;
            _twoSecDelay = 2;
            _fourSecDelay = 4;
            _stoppingDistance = 0.1f;
        }

        public override void Died()
        {
            IsDied = true;
            _playerScore.Change(_score.Count);

            if (_audioSource.enabled == true)
            {
                _audioSource.Play();
            }

            StartCoroutine(DiedCoroutine());
        }

        public void ReviveZombie()
        {
            IsDied = false;
        }

        private IEnumerator DiedCoroutine()
        {
            OnDied?.Invoke();
            _enemyBlink.DisableBlink();
            _triggerCollider.enabled = false;
            _capsuleCollider.enabled = false;
            _movement.enabled = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _rigidbody.isKinematic = true;
            Animator.SetBool(_animatorData.Died, true);
            _zombieAttack.StopAttackCoroutine();

            yield return new WaitForSeconds(_fourSecDelay);

            transform.Translate(0, _heightOfFall * Time.deltaTime, 0);
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + _heightOfFall, startPosition.z);
            float progress = 0;
            float step = 0.0001f;

            while (Vector3.Distance(transform.position, endPosition) > _stoppingDistance)
            {
                transform.position = Vector3.Lerp(transform.position, endPosition, progress);
                progress += step;
                yield return null;
            }

            yield return new WaitForSeconds(_twoSecDelay);

            transform.position = new Vector3(924, 483, 0);
            _enemyBlink.EnableBlink();
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            gameObject.SetActive(false);
            Animator.SetBool(_animatorData.Died, false);
            _rigidbody.isKinematic = false;
            _triggerCollider.enabled = true;
            _capsuleCollider.enabled = true;
            _movement.enabled = true;
        }
    }
}