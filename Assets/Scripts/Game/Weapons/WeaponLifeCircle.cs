using System.Collections;
using CommonVariables;
using DG.Tweening;
using UnityEngine;

namespace Game.Weapons
{
    public class WeaponLifeCircle : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private int _duration;
        [SerializeField] private int _durationValue;

        private Variables _variables;
        private WaitForSeconds _durationOfAttack;
        private Vector3 _nextValue;

        private void Awake()
        {
            _variables = new Variables();
            _durationOfAttack = new WaitForSeconds(10);
            _variables.ChangeDurationOfReloading(_durationValue);
            _nextValue = new Vector3(0, 360, 0);
        }

        private void Start()
        {
            transform.DORotate(
                _nextValue,
                _duration,
                RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetLink(gameObject);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform, true);
        }

        public void Run()
        {
            StartCoroutine(MoveWeapon());
        }

        private IEnumerator MoveWeapon()
        {
            while (enabled)
            {
                yield return null;

                ShowAllWeapon();

                yield return _durationOfAttack;

                if (_durationValue > 0)
                {
                    HideAllWeapon();
                }

                yield return _variables.DurationOfReloading;
            }

            HideAllWeapon();
        }

        protected virtual void ShowAllWeapon()
        {
            foreach (Weapon weapon in _weapons)
            {
                weapon.Show();
            }
        }

        protected virtual void HideAllWeapon()
        {
            foreach (Weapon weapon in _weapons)
            {
                weapon.Hide();
            }
        }

        public void ChangeDurationOfReloading(int value)
        {
            _variables.ChangeDurationOfReloading(value);
            _durationValue = value;
        }
    }
}