using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponLifeCyrcle : WeaponLifeCircle
{
    [SerializeField] private Attack[] _attacks;

    protected override void ShowAllWeapon()
    {
        base.ShowAllWeapon();

        if (_attacks.Length > 0)
        {
            for (int i = 0; i < _attacks.Length; i++)
            {
                _attacks[i].Enable();
            }
        }
    }

    protected override void HideAllWeapon()
    {
        base.HideAllWeapon();

        if (_attacks.Length > 0)
        {
            for (int i = 0; i < _attacks.Length; i++)
            {
                _attacks[i].Disable();
            }
        }
    }
}
