using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private GameObject _player;
    private bool _isInZone;
    private Vector3 _targetPlace;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnDisable()
    {
        _isInZone = false;
    }

    private void Update()
    {
        if (!_isInZone)
        {
            transform.LookAt(_targetPlace);
            transform.position = Vector3.MoveTowards(transform.position, _targetPlace, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPlace) < 0.1f)
            {
                _isInZone = true;
            }

            return;
        }
        
        if (Vector3.Distance(transform.position, _player.transform.position) < 0.5f)
        {
            return;
        }

        transform.LookAt(_player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
    }

    public void SetTargetPlace(Vector3 targetPlace)
    {
        _targetPlace = targetPlace;
    }
}
