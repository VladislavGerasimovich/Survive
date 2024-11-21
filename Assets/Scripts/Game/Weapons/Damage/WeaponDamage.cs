using UnityEngine;

namespace Game.Weapons.Damage
{
    public class WeaponDamage : MonoBehaviour
    {
        [SerializeField] protected int Damage;

        public int Harm { get; private set; }

        private void Awake()
        {
            Harm = Damage;
        }

        public void Change(int damage)
        {
            Harm = damage;
        }
    }
}