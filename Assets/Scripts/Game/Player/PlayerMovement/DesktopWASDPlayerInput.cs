using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class DesktopWASDPlayerInput : Input
{
    private PlayerInput _playerInput;
    private InputAction _inputAction;
    private string _actionName;

    private void Awake()
    {
        _actionName = "Move";
        _playerInput = GetComponent<PlayerInput>();
        EnableInput();
    }

    private void Start()
    {
        _inputAction = _playerInput.actions.FindAction(_actionName);
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(IsCan == true)
        {
            Vector2 direction = _inputAction.ReadValue<Vector2>();
            SetMovementPosition(direction);
        }
    }
}
