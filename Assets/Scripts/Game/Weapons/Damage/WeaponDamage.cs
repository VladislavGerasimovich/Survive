using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] protected int _damage;

    public int Damage { get; private set; }

    private void Awake()
    {
        Damage = _damage;
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }
}
