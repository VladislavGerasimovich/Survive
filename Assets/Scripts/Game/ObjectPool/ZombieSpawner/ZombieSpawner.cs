using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _startPositions;
    [SerializeField] private List<GameObject> _targetPositions;
    [SerializeField] private List<GameObject> _firstZombiePositions;
    [SerializeField] private ZombiesPools _zombiesPools;

    private int _firstZombieIndex;
    private List<int> _typesOfZombies;
    private WaitForSeconds _delay;
    private float _pastTime;
    private int _firstTimeToIncreaseDifficulty;
    private int _secondTimeToIncreaseDifficulty;
    private int _thirdTimeToIncreaseDifficulty;
    private int _fourthTimeToIncreaseDifficulty;
    private int _fifthTimeToIncreaseDifficulty;

    private void Awake()
    {
        _firstZombieIndex = 1;
        _firstTimeToIncreaseDifficulty = 30;
        _secondTimeToIncreaseDifficulty = 60;
        _thirdTimeToIncreaseDifficulty = 90;
        _fourthTimeToIncreaseDifficulty = 120;
        _fifthTimeToIncreaseDifficulty = 150;
        _typesOfZombies = new List<int>() {1, 1, 1, 2};
        _delay = new WaitForSeconds(5);
    }

    private void Start()
    {
        for (int i = 0; i < _firstZombiePositions.Count; i++)
        {
            CreateZombie(_firstZombieIndex, _firstZombiePositions[i].transform.position, _firstZombiePositions[i].transform.position);
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

            for (int i = _startPositions.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                GameObject tempStartPosition = _startPositions[j];
                _startPositions[j] = _startPositions[i];
                _startPositions[i] = tempStartPosition;
                GameObject tempTargetPosition = _targetPositions[j];
                _targetPositions[j] = _targetPositions[i];
                _targetPositions[i] = tempTargetPosition;
            }

            for (int i = 0; i < _startPositions.Count; i++)
            {
                CreateZombie(_typesOfZombies[i], _startPositions[i].transform.position, _targetPositions[i].transform.position);
            }

            SetValues();

            yield return _delay;
        }
    }

    private void CreateZombie(int typeOfZombies, Vector3 position, Vector3 targetPosition)
    {
        if (typeOfZombies == 1)
        {
            _zombiesPools.TryGetSimpleZombie(out GameObject zombie);
            zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
            zombie.transform.position = position;
            zombie.SetActive(true);
        }
        else if (typeOfZombies == 2)
        {
            _zombiesPools.TryGetFastZombie(out GameObject zombie);
            zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
            zombie.transform.position = position;
            zombie.SetActive(true);
        }
        else if (typeOfZombies == 3)
        {
            _zombiesPools.TryGetBigZombie(out GameObject zombie);
            zombie.GetComponent<ZombieMovement>().SetTargetPlace(targetPosition);
            zombie.transform.position = position;
            zombie.SetActive(true);
        }
    }

    private void SetValues()
    {
        if (_pastTime >= _firstTimeToIncreaseDifficulty && _pastTime < _secondTimeToIncreaseDifficulty)
        {
            _typesOfZombies = new List<int>() { 1, 1, 2, 2 };
        }
        else if (_pastTime >= _secondTimeToIncreaseDifficulty && _pastTime < _thirdTimeToIncreaseDifficulty)
        {
            _typesOfZombies = new List<int>() { 1, 2, 2, 3 };
        }
        else if (_pastTime >= _thirdTimeToIncreaseDifficulty && _pastTime < _fourthTimeToIncreaseDifficulty)
        {
            _delay = new WaitForSeconds(4);
        }
        else if (_pastTime >= _fourthTimeToIncreaseDifficulty && _pastTime < _fifthTimeToIncreaseDifficulty)
        {
            _typesOfZombies = new List<int>() { 2, 2, 3, 3 };
        }
        else if (_pastTime >= _fifthTimeToIncreaseDifficulty)
        {
            _delay = new WaitForSeconds(3);
        }
    }
}