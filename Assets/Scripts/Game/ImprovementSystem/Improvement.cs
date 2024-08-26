using System;

public class Improvement
{
    private int[] _values;
    private int _currentLevel;
    private int _maxLevel;

    public string Type { get; private set; }    

    public Improvement(int[] values, int currentLevel, int maxLevel, string type)
    {
        _values = values;
        _currentLevel = currentLevel;
        _maxLevel = maxLevel;
        Type = type;
    }

    public event Action<int, string> LevelIncreased;
    public event Action<string> MaxLevelReached;

    public void IncreaseLevel()
    {
        _currentLevel++;
        GetValue(out int value);
        LevelIncreased?.Invoke(value, Type);

        if(_currentLevel == _maxLevel - 1)
        {
            MaxLevelReached?.Invoke(Type);
        }
    }

    private void GetValue(out int value)
    {
        value = _values[_currentLevel];
    }
}
