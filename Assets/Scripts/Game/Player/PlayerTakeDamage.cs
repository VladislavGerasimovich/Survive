using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public event Action<int> TakeDamage;

    public void Take(int damage)
    {
        TakeDamage.Invoke(damage);
    }
}
