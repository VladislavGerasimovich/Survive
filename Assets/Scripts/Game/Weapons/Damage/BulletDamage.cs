using UnityEngine;

public class BulletDamage : WeaponDamage
{
    [SerializeField] private Bullet _bullet;

    public void Collision()
    {
        _bullet.Died();
    }
}
