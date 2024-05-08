using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class DesktopMousePlayerInput : Input
{
    private bool _isMoving;

    private void Awake()
    {
        EnableInput();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            if(IsCan == true)
            {
                OnFingerDown(UnityEngine.Input.mousePosition);
                _isMoving = true;
            }
        }

        if(_isMoving == true)
        {
            OnFingerMove();
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            _isMoving = false;

            OnFingerUp();
        }
    }

    public override void OnFingerMove()
    {
        Vector2 handlerPosition;
        float maxMovement = _joystick.Size.x / 2;
        Vector2 mousePosition = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);

        if (Vector2.Distance(mousePosition, _joystick.Position) > maxMovement)
        {
            handlerPosition = (mousePosition - _joystick.Position).normalized * maxMovement;
        }
        else
        {
            handlerPosition = mousePosition - _joystick.Position;
        }

        _joystick.SetHandlerPosition(handlerPosition);
        SetMovementPosition(handlerPosition / maxMovement);
    }

    public override void OnFingerUp()
    {
        _joystick.SetHandlerPosition(Vector2.zero);
        _joystick.Disable();
        SetMovementPosition(Vector2.zero);
    }

    public override void OnFingerDown(Vector2 position)
    {
        _joystick.Enable();
        _joystick.SetPosition(ClampStartPosition(position));
    }
}
