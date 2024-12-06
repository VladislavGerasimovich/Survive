using System.Collections;
using System.Collections.Generic;
using CommonVariables;
using Game.Zombie;
using UnityEngine;

namespace Game.ObjectPools.ZombiePools
{
    public class ZombieSpawner : MonoBehaviour
    {
        private const int ZEROZOMBIETYPE = 0;
        private const int FIRSTZOMBIETYPE = 1;
        private const int SECONDZOMBIETYPE = 2;
        private const int THIRDZOMBIETYPE = 3;
        private const int FIRSTDELAYVALUE = 3;
        private const int SECONDDELAYVALUE = 2;

        [SerializeField] private List<GameObject> _startPositions;
        [SerializeField] private List<GameObject> _targetPositions;
        [SerializeField] private List<GameObject> _firstZombiePositions;
        [SerializeField] private ZombiesPools _zombiesPools;

        private int _startPositionsCountMultiplier;
        private int _minSelectableStartPosition;
        private int _randomMultiplier;
        private int _firstZombieIndex;
        private List<int> _typesOfZombies;
        private int _currentDifficultyLevel;
        private List<List<int>> _firstDifficultyLevel;
        private List<List<int>> _secondDifficultyLevel;
        private List<List<List<int>>> _typesOfZombiesDependingOfDifficultyLevel;
        private Variables _variables;
        private float _pastTime;
        private int _firstTimeToIncreaseDifficulty;
        private int _secondTimeToIncreaseDifficulty;
        private int _thirdTimeToIncreaseDifficulty;
        private int _fourthTimeToIncreaseDifficulty;
        private int _fifthTimeToIncreaseDifficulty;

        private void Awake()
        {
            _startPositionsCountMultiplier = 1;
            _minSelectableStartPosition = 1;
            _randomMultiplier = 1;
            _currentDifficultyLevel = PlayerPrefs.GetInt(Constants.Level, 0);
            _firstZombieIndex = 1;
            _firstTimeToIncreaseDifficulty = 30;
            _secondTimeToIncreaseDifficulty = 60;
            _thirdTimeToIncreaseDifficulty = 90;
            _fourthTimeToIncreaseDifficulty = 120;
            _fifthTimeToIncreaseDifficulty = 150;
            _typesOfZombiesDependingOfDifficultyLevel = new List<List<List<int>>>();
            _firstDifficultyLevel = new List<List<int>>() {
                new List<int> { 1, 1, 1, 2 },
                new List<int> { 1, 1, 2, 2 },
                new List<int> { 1, 2, 2, 2 } };
            _typesOfZombiesDependingOfDifficultyLevel.Add(_firstDifficultyLevel);
            _secondDifficultyLevel = new List<List<int>>() {
                new List<int> { 1, 2, 2, 3 },
                new List<int> { 1, 2, 3, 3 },
                new List<int> { 2, 2, 3, 3 } };
            _typesOfZombiesDependingOfDifficultyLevel.Add(_secondDifficultyLevel);
            _typesOfZombies = _typesOfZombiesDependingOfDifficultyLevel[_currentDifficultyLevel][0];
            _variables = new Variables();
            _variables.ChangeDelay(5);
        }

        private void Start()
        {
            for (int i = 0; i < _firstZombiePositions.Count; i++)
            {
                CreateZombie(
                    _firstZombieIndex,
                    _firstZombiePositions[i].transform.position,
                    _firstZombiePositions[i].transform.position);
            }

            StartCoroutine(Run());
        }

        private void Update()
        {
            _pastTime += Time.deltaTime;
        }

        private IEnumerator Run()
        {
            while (enabled)
            {
                System.Random random = new System.Random();

                for (int i = _startPositions.Count - _startPositionsCountMultiplier;
                    i >= _minSelectableStartPosition;
                    i--)
                {
                    int j = random.Next(i + _randomMultiplier);
                    GameObject tempStartPosition = _startPositions[j];
                    _startPositions[j] = _startPositions[i];
                    _startPositions[i] = tempStartPosition;
                    GameObject tempTargetPosition = _targetPositions[j];
                    _targetPositions[j] = _targetPositions[i];
                    _targetPositions[i] = tempTargetPosition;
                }

                for (int i = 0; i < _startPositions.Count; i++)
                {
                    CreateZombie(_typesOfZombies[i],
                        _startPositions[i].transform.position,
                        _targetPositions[i].transform.position);
                }

                SetValues();

                yield return _variables.Delay;
            }
        }

        private void CreateZombie(int typeOfZombies, Vector3 position, Vector3 targetPosition)
        {
            if (typeOfZombies == FIRSTZOMBIETYPE)
            {
                _zombiesPools.TryGetSimpleZombie(out GameObject zombie);
                zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
                zombie.transform.position = position;
                zombie.SetActive(true);
            }
            else if (typeOfZombies == SECONDZOMBIETYPE)
            {
                _zombiesPools.TryGetFastZombie(out GameObject zombie);
                zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
                zombie.transform.position = position;
                zombie.SetActive(true);
            }
            else if (typeOfZombies == THIRDZOMBIETYPE)
            {
                _zombiesPools.TryGetBigZombie(out GameObject zombie);
                zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
                zombie.transform.position = position;
                zombie.SetActive(true);
            }
        }

        private void SetValues()
        {
            if (_pastTime < _firstTimeToIncreaseDifficulty)
            {
                _typesOfZombies = _typesOfZombiesDependingOfDifficultyLevel[_currentDifficultyLevel][ZEROZOMBIETYPE];
            }
            else if (_pastTime >= _firstTimeToIncreaseDifficulty && _pastTime < _secondTimeToIncreaseDifficulty)
            {
                _typesOfZombies = _typesOfZombiesDependingOfDifficultyLevel[_currentDifficultyLevel][FIRSTZOMBIETYPE];
                _variables.ChangeDelay(FIRSTDELAYVALUE);
            }
            else if (_pastTime >= _secondTimeToIncreaseDifficulty && _pastTime < _thirdTimeToIncreaseDifficulty)
            {
                _typesOfZombies = _typesOfZombiesDependingOfDifficultyLevel[_currentDifficultyLevel][SECONDZOMBIETYPE];
            }
            else if (_pastTime >= _thirdTimeToIncreaseDifficulty && _pastTime < _fourthTimeToIncreaseDifficulty)
            {
            }
            else if (_pastTime >= _fourthTimeToIncreaseDifficulty && _pastTime < _fifthTimeToIncreaseDifficulty)
            {
                _variables.ChangeDelay(SECONDDELAYVALUE);
            }
        }
    }
}