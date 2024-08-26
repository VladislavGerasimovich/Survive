using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _rank;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _name;

    public void SetRank(string text)
    {
        _rank.text = text;
    }

    public void SetScore(string text)
    {
        _score.text = text;
    }

    public void SetName(string text)
    {
        _name.text = text;
    }
}
