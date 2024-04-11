using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private Transform _entryContainer;
    [SerializeField] private Transform _entryTemplate;

    //private List<HighScoreEntry> _highScoreEntryList;
    private List<Transform> _highScoreEntryTransformList;

    private void Awake()
    {
        //AddHighScoreEntry(676767676, "Vlad");

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        _highScoreEntryTransformList = new List<Transform>();

        for (int i = 0; i < highScores.HighScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highScores.HighScoreEntryList.Count; j++)
            {
                if (highScores.HighScoreEntryList[j].Score > highScores.HighScoreEntryList[i].Score)
                {
                    HighScoreEntry highScoreEntry = highScores.HighScoreEntryList[i];
                    highScores.HighScoreEntryList[i] = highScores.HighScoreEntryList[j];
                    highScores.HighScoreEntryList[j] = highScoreEntry;
                }
            }
        }

        foreach (HighScoreEntry highScoreEntry in highScores.HighScoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, _highScoreEntryTransformList);
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(_entryTemplate, _entryContainer);

        int rank = transformList.Count + 1;
        entryTransform.GetComponent<PlayerInfo>().SetRank(rank.ToString());

        int score = highScoreEntry.Score;
        entryTransform.GetComponent<PlayerInfo>().SetScore(score.ToString());

        string name = highScoreEntry.Name;
        entryTransform.GetComponent<PlayerInfo>().SetName(name.ToString());

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        HighScoreEntry highScoreEntry = new HighScoreEntry { Score = score, Name = name };

        string jsonString = PlayerPrefs.GetString("highScoreTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

        highScores.HighScoreEntryList.Add(highScoreEntry);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<HighScoreEntry> HighScoreEntryList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int Score;
        public string Name;
    }
}


