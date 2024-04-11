using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(ZombieMovement))]
[RequireComponent(typeof(Score))]
[RequireComponent(typeof(EnemyBlink))]
[RequireComponent (typeof(ZombieAttack))]
public class ZombieDied : Die
{
    [SerializeField] private CapsuleCollider _triggerCollider;

    private PlayerScore _playerScore;
    private CapsuleCollider _capsuleCollider;
    private ZombieMovement _movement;
    private Rigidbody _rigidbody;
    private Score _score;
    private EnemyBlink _enemyBlink;
    private ZombieAttack _zombieAttack;

    public event Action Rip;

    private void Awake()
    {
        _playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<PlayerScore>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _movement = GetComponent<ZombieMovement>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _score = GetComponent<Score>();
        _enemyBlink = GetComponent<EnemyBlink>();
        _zombieAttack = GetComponent<ZombieAttack>();
    }

    public override void Died()
    {
        _playerScore.SetScore(_score.Count);
        StartCoroutine(DiedCoroutine());
    }

    private IEnumerator DiedCoroutine()
    {
        Rip?.Invoke();
        _enemyBlink.DisableBlink();
        _triggerCollider.enabled = false;
        _capsuleCollider.enabled = false;
        _movement.enabled = false;
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.isKinematic = true;
        _animator.SetBool("Died", true);
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

        transform.position = startPosition;
        _enemyBlink.EnableBlink();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        gameObject.SetActive(false);
        _animator.SetBool("Died", false);
        _rigidbody.isKinematic = false;
        _triggerCollider.enabled = true;
        _capsuleCollider.enabled = true;
        _movement.enabled = true;
    }
}
