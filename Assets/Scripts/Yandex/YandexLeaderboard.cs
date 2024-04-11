using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const string English = "en";
    private const string Turkish = "tr";
    private const string Russian = "ru";

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private string _englishText;
    [SerializeField] private string _turkishText;
    [SerializeField] private string _russianText;

    private string AnonymousName;

    private void Start()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        ChangeLanguage(languageCode);
    }

    public void ChangeLanguage(string languageCode)
    {
        switch (languageCode)
        {
            case English:
                AnonymousName = _englishText;
                break;
            case Turkish:
                AnonymousName = _turkishText;
                break;
            case Russian:
                AnonymousName = _russianText;
                break;
        }
    }

    public void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
            {
                Leaderboard.SetScore(LeaderboardName, score);
            }
        });
    }

    public void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if(string.IsNullOrEmpty(name))
                {
                    name = AnonymousName;
                }

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardView.Construct(_leaderboardPlayers);
        });
    }
}
