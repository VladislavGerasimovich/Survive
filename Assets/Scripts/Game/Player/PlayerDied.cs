using UnityEngine;

public class PlayerDied : Die
{
    [SerializeField] private EndGamePanel _endGamePanel;

    public override void Died()
    {
        _endGamePanel.Open();
    }
}