using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapons
{
    public class ThrowingWeapon : MonoBehaviour
    {
        [SerializeField] private List<GrenadeTrajectory> _grenadesTrajectory;

        public void Run()
        {
            foreach (GrenadeTrajectory grenadeTrajectory in _grenadesTrajectory)
            {
                grenadeTrajectory.Throw();
            }
        }
    }
}