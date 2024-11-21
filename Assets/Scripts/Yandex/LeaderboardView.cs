using System.Collections.Generic;
using UnityEngine;

namespace YandexElements
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

        private List<LeaderboardElement> _spawnedElements = new();

        public void Construct(List<LeaderboardPlayer> leaderboardPlayers)
        {
            Clear();

            foreach (LeaderboardPlayer player in leaderboardPlayers)
            {
                LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);
                leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Score);

                _spawnedElements.Add(leaderboardElementInstance);
            }
        }

        private void Clear()
        {
            foreach (var element in _spawnedElements)
            {
                Destroy(element);
            }

            _spawnedElements = new List<LeaderboardElement>();
        }
    }
}