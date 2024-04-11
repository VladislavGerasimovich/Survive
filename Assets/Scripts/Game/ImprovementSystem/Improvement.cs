using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Improvement
{
    private int[] _values;
    private int _currentLevel;

    public string Type { get; private set; }    

    public Improvement(int[] values, int currentLevel, string type)
    {
        _values = values;
        _currentLevel = currentLevel;
        Type = type;
    }

    public event Action<int, string> LevelIncreased;
    public event Action<string> MaxLevelReached;

    public void IncreaseLevel()
    {
        _currentLevel++;
        GetValue(out int value);
        LevelIncreased?.Invoke(value, Type);

        if(_currentLevel == _values.Length - 1)
        {
            MaxLevelReached?.Invoke(Type);
        }
    }

    private void GetValue(out int value)
    {
        value = _values[_currentLevel];
    }
}