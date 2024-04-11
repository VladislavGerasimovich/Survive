using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsContainer : MonoBehaviour
{
    private List<BulletDamage> _bulletDamages;

    private void Awake()
    {
        _bulletDamages = new List<BulletDamage>();
    }

    private void Start()
    {
        FindPoolObjects();
    }

    public virtual void SetPoolObjectDamage(int value)
    {
        foreach (BulletDamage bullet in _bulletDamages)
        {
            Debug.Log(value + " урон пули");
            bullet.SetDamage(value);
        }
    }

    public virtual void FindPoolObjects()
    {
        foreach (Transform child in transform)
        {
            _bulletDamages.Add(child.GetComponent<BulletDamage>());
        }
    }
}
