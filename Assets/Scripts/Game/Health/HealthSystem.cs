using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int _value;
    private int _maxValue;
    private float _currentValue;

    public bool IsImmortal;

    public event Action Died;
    public event Action<float> OnValueChanged;

    public HealthSystem(int value)
    {
        _maxValue = value;
        _value = value;
    }

    public void TakeDamage(int damage)
    {
        if(IsImmortal == false)
        {
            _value -= damage;
            _currentValue = (float)_value / _maxValue;
            OnValueChanged?.Invoke(_currentValue);

            if (_value < 0)
            {
                _value = 0;
            }

            if(_value == 0)
            {
                Died?.Invoke();
                _value = _maxValue;
            }
        }
    }

    public void Restore()
    {
        _value = _maxValue;
        _currentValue = (float)_value / _maxValue;
        OnValueChanged?.Invoke(_currentValue);
    }

    public void MakeImmortal()
    {
        IsImmortal = true;
    }

    public void MakeMortal()
    {
        IsImmortal = false;
    }
}
