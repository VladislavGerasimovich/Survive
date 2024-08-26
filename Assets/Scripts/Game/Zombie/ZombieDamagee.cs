using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamagee : MonoBehaviour
{
    [SerializeField] protected int Damage;

    public int Harm { get; private set; }

    private void Awake()
    {
        Harm = Damage;
    }

    public void SetDamage(int damage)
    {
        Harm = damage;
    }
}
