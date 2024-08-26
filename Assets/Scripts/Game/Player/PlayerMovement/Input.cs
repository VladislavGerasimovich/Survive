using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Input : MonoBehaviour
{
    [SerializeField] public Joystick _joystick;

    public bool IsCan { get; private set; }
    public Vector2 MovementAmount { get; private set; }

    public void EnableInput()
    {
        IsCan = true;

        if (_joystick != null)
        {
            _joystick.Enable();
        }
    }

    public void DisableInput()
    {
        IsCan = false;

        if(_joystick != null)
        {
            _joystick.Disable();
        }
    }

    public void SetMovementPosition(Vector2 movementAmount)
    {
        MovementAmount = movementAmount;
    }

    public virtual void OnFingerMove(Finger finger){}

    public virtual void OnFingerMove(){}

    public virtual void OnFingerUp(Finger finger){}

    public virtual void OnFingerUp(){}

    public virtual void OnFingerDown(Finger finger){}

    public virtual void OnFingerDown(Vector2 position){}

    public Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < _joystick.Size.x / 2)
        {
            startPosition.x = _joystick.Size.x / 2;
        }

        if (startPosition.y < _joystick.Size.y / 2)
        {
            startPosition.y = _joystick.Size.y / 2;
        }
        else if (startPosition.y > UnityEngine.Screen.height - _joystick.Size.y / 2)
        {
            startPosition.y = UnityEngine.Screen.height - _joystick.Size.y / 2;
        }

        return startPosition;
    }
}
