using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace YandexElements
{
    public class YandexLeaderboard : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private string _englishText;
        [SerializeField] private string _turkishText;
        [SerializeField] private string _russianText;

        private string AnonymousName;

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

        private void Start()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            ChangeLanguage(languageCode);
        }

        public void ChangeLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case Constants.English:
                    AnonymousName = _englishText;
                    break;
                case Constants.Turkish:
                    AnonymousName = _turkishText;
                    break;
                case Constants.Russian:
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

            Leaderboard.GetPlayerEntry(Constants.LeaderboardName, (result) =>
            {
                if (result == null || result.score < score)
                {
                    Leaderboard.SetScore(Constants.LeaderboardName, score);
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

            Leaderboard.GetEntries(Constants.LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = AnonymousName;
                    }

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                _leaderboardView.Construct(_leaderboardPlayers);
            });
        }
    }
}