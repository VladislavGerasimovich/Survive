using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Zombie
{
    public class ZombieDamage : MonoBehaviour
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