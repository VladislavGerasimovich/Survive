using System.Collections;
using CommonVariables;
using Game.ObjectPools;
using UnityEngine;

namespace Game.Weapons
{
    public class GrenadeTrajectory : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _angle;
        [SerializeField] private GrenadesCreator _grenadesCreator;

        private int _startValueOfDuration;
        private int _delayBeforeThrowing;
        private int _power;
        private int _tangentMultiplier;
        private int _piMultiplier;
        private float _gravity = Physics.gravity.y;
        private Variables _variables;

        private void Awake()
        {
            _startValueOfDuration = 18;
            _tangentMultiplier = 2;
            _power = 2;
            _piMultiplier = 180;
            _delayBeforeThrowing = 1;
            _variables = new Variables();
            _variables.ChangeDurationOfReloading(_startValueOfDuration);
        }

        public void Throw()
        {
            StartCoroutine(ThrowCoroutine());
        }

        public void ChangeDurationOfReloading(int value)
        {
            _variables.ChangeDurationOfReloading(value);
        }

        private IEnumerator ThrowCoroutine()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(_delayBeforeThrowing);

                Shot();

                yield return _variables.DurationOfReloading;
            }
        }

        private void Shot()
        {
            Vector3 direction = _targetPoint.position - transform.position;
            Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

            transform.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);

            float x = directionXZ.magnitude;
            float y = direction.y;

            float angleInRadians = _angle * Mathf.PI / _piMultiplier;

            float v2 =
                (_gravity * x * x)
                / (_tangentMultiplier * (y - Mathf.Tan(angleInRadians) * x)
                * Mathf.Pow(Mathf.Cos(angleInRadians), _power));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            _grenadesCreator.TryGet(out GameObject grenade);

            if (grenade != null)
            {
                grenade.SetActive(true);
                grenade.GetComponent<Grenade>().ShowGrenadePrefab(transform.position, transform.forward * v);
            }
        }
    }
}