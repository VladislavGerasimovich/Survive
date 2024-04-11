using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDied : Die
{
    [SerializeField] private EndGamePanel _endGamePanel;

    public override void Died()
    {
        _endGamePanel.Open();
    }
}