using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobilePlayerInput))]
[RequireComponent(typeof(DesktopWASDPlayerInput))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private Animator _animator;
    private AnimatorData _animatorData;
    private CharacterController _characterController;
    private Input _playerInput;
    private MobilePlayerInput _mobilePlayerInput;
    private DesktopWASDPlayerInput _desktopPlayerInput;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animatorData = new AnimatorData();
        _characterController = GetComponent<CharacterController>();
        _mobilePlayerInput = GetComponent<MobilePlayerInput>();
        _desktopPlayerInput = GetComponent<DesktopWASDPlayerInput>();
        
        _playerInput = _desktopPlayerInput;
        _desktopPlayerInput.enabled = true;

        if (Application.isMobilePlatform == true)
        {
            _playerInput = _mobilePlayerInput;
            _mobilePlayerInput.enabled = true;
            _desktopPlayerInput.enabled = false;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetFloat("moveX", _playerInput.MovementAmount.x);
        _animator.SetFloat("moveZ", _playerInput.MovementAmount.y);

        if(_playerInput.MovementAmount.sqrMagnitude < 0.01f)
        {
            return;
        }

        float _scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(_playerInput.MovementAmount.x, 0f, _playerInput.MovementAmount.y);
        Vector3 targetDirection = Vector3.RotateTowards(_characterController.transform.forward, offset, _rotationSpeed * Time.deltaTime, 0.0f);
        _characterController.transform.rotation = Quaternion.LookRotation(targetDirection);
        _characterController.Move(offset * _scaledMoveSpeed);
    }
}

public class AnimatorData
{
    public readonly int MoveX = Animator.StringToHash(nameof(MoveX));
    public readonly int MoveZ = Animator.StringToHash(nameof(MoveZ));
    public readonly int Died = Animator.StringToHash(nameof(Died));
}
