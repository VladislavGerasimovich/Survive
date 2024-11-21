using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Movement
{
    [RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
    public class DesktopWASDPlayerInput : PlayerInput
    {
        private UnityEngine.InputSystem.PlayerInput _playerInput;
        private InputAction _inputAction;
        private string _actionName;

        private void Awake()
        {
            _actionName = "Move";
            _playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
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
            if (IsCan == true)
            {
                Vector2 direction = _inputAction.ReadValue<Vector2>();
                SetMovementPosition(direction);
            }
        }
    }
}