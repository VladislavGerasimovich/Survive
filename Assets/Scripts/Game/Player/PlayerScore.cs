using UnityEngine;

namespace Game.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public int Score { get; private set; }

        private void Awake()
        {
            Score = 0;
        }

        public void Change(int value)
        {
            Score += value;
        }
    }
}