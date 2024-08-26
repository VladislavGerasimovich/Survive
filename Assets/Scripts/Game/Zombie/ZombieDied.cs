using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(ZombieMovement))]
[RequireComponent(typeof(Score))]
[RequireComponent(typeof(EnemyBlink))]
[RequireComponent (typeof(ZombieAttack))]
public class ZombieDied : Die
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

    public bool IsDied { get; private set; }

    public event Action OnDied;

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
    }

    public override void Died()
    {
        IsDied = true;
        _playerScore.SetScore(_score.Count);

        if(_audioSource.enabled == true)
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
        Animator.SetBool("Died", true);
        _zombieAttack.StopAttackCoroutine();

        yield return new WaitForSeconds(4);

        transform.Translate(0, -2 * Time.deltaTime, 0);
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y - 2, startPosition.z);
        float progress = 0;
        float step = 0.0001f;

        while (Vector3.Distance(transform.position, endPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, progress);
            progress += step;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        transform.position = new Vector3(924, 483, 0);
        _enemyBlink.EnableBlink();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        gameObject.SetActive(false);
        Animator.SetBool("Died", false);
        _rigidbody.isKinematic = false;
        _triggerCollider.enabled = true;
        _capsuleCollider.enabled = true;
        _movement.enabled = true;
    }
}
