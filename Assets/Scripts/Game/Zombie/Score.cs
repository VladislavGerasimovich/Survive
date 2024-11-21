using UnityEngine;

namespace Game.Zombie
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private int _count;

        public int Count => _count;
    }
}