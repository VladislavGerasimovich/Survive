using UnityEngine;

namespace Game.Weapons.Damage
{
    public class BulletDamage : WeaponDamage
    {
        [SerializeField] private Bullet _bullet;

        public void Collision()
        {
            _bullet.Died();
        }
    }
}