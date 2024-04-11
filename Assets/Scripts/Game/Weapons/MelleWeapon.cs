using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleWeapon : Weapon
{
    [SerializeField] private CapsuleCollider _collider;

    public override void Show()
    {
        base.Show();
        _collider.enabled = true;
    }

    public override void Hide()
    {
        base.Hide();
        _collider.enabled = false;
    }
}
