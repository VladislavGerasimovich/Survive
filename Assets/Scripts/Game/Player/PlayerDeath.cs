using UnityEngine;
using Game.UI.Screens;

namespace Game.Player
{
    public class PlayerDeath : DeathOfAllCharacters.Death
    {
        [SerializeField] private EndGamePanel _endGamePanel;

        public override void Died()
        {
            _endGamePanel.Open();
        }
    }
}