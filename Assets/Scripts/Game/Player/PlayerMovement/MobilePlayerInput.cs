using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class MobilePlayerInput : Input
{
    private Finger _movementFinger;

    private void Awake()
    {
        EnableInput();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerUp += OnFingerUp;
        ETouch.Touch.onFingerMove += OnFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerUp -= OnFingerUp;
        ETouch.Touch.onFingerMove -= OnFingerMove;
        EnhancedTouchSupport.Disable();
    }

    public override void OnFingerMove(Finger finger)
    {
        if(finger == _movementFinger)
        {
            Vector2 handlerPosition;
            float maxMovement = _joystick.Size.x / 2;
            ETouch.Touch currentTouch = finger.currentTouch;

            if(Vector2.Distance(currentTouch.screenPosition, _joystick.Position) > maxMovement)
            {
                handlerPosition = (currentTouch.screenPosition - _joystick.Position).normalized * maxMovement;
            }
            else
            {
                handlerPosition = currentTouch.screenPosition - _joystick.Position;
            }

            _joystick.SetHandlerPosition(handlerPosition);
            SetMovementPosition(handlerPosition / maxMovement);
        }
    }

    public override void OnFingerUp(Finger finger)
    {
        if(finger == _movementFinger)
        {
            _movementFinger = null;
            _joystick.SetHandlerPosition(Vector2.zero);
            _joystick.Disable();
            SetMovementPosition(Vector2.zero);
        }
    }

    public override void OnFingerDown(Finger finger)
    {
        if(_movementFinger == null && IsCan == true)
        {
            _movementFinger = finger;
            SetMovementPosition(Vector2.zero);
            _joystick.Enable();
            _joystick.SetPosition(ClampStartPosition(finger.screenPosition));
        }
    }
}
