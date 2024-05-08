using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
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
        Vector2 direction = _inputAction.ReadValue<Vector2>();
        SetMovementPosition(direction);
    }
}
