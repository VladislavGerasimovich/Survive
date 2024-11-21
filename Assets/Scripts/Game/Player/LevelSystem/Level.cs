using System;

namespace Game.Player.Levels
{
    public class Level
    {
        private int _level;
        private int _maxExperience;
        private int _currentExperience;
        private int _remainingExperience;

        public Level()
        {
            _level = 1;
            _maxExperience = 5;
            _currentExperience = 0;
        }

        public event Action<float> CurrentValueExperienceChange;
        public event Action<int> LevelValueChange;

        public void GetLevel(out int level)
        {
            level = _level;
        }

        public void SetExperience(int experience)
        {
            _currentExperience += experience;

            if (_currentExperience >= _maxExperience)
            {
                _remainingExperience = _currentExperience - _maxExperience;
                _maxExperience += 40;
                _level++;
                _currentExperience = _remainingExperience;
                _remainingExperience = 0;
                LevelValueChange?.Invoke(_level);
            }

            float percentageValue = (float)_currentExperience / _maxExperience;
            CurrentValueExperienceChange?.Invoke(percentageValue);
        }
    }
}