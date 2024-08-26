using System.Collections;
using UnityEngine;

public class GrenadeTrajectory : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _angle;
    [SerializeField] private GrenadesCreator _grenadesCreator;

    private float _gravity = Physics.gravity.y;
    private int _durationOfReloading;

    private void Awake()
    {
        _durationOfReloading = 18;
    }

    public void Throw()
    {
        StartCoroutine(ThrowCoroutine());
    }

    public void ChangeDurationOfReloading(int value)
    {
        _durationOfReloading = value;
    }


    private IEnumerator ThrowCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1);

            Shot();

            yield return new WaitForSeconds(_durationOfReloading);
        }
    }

    private void Shot()
    {
        Vector3 direction = _targetPoint.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        transform.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);

        float x = directionXZ.magnitude;
        float y = direction.y;

        float angleInRadians = _angle * Mathf.PI / 180;

        float v2 = (_gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        _grenadesCreator.TryGetObject(out GameObject grenade);

        if(grenade != null)
        {
            grenade.SetActive(true);
            grenade.GetComponent<Grenade>().ShowGrenadePrefab(transform.position, transform.forward * v);
        }
    }
}
