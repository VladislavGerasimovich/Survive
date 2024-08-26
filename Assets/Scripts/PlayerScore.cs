using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int Score { get; private set; }

    private void Awake()
    {
        Score = 0;
    }

    public void SetScore(int value)
    {
        Score += value;
    }
}
