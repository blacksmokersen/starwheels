using UnityEngine;

namespace SW.Matchmaking
{
    [DisallowMultipleComponent]
    public class MatchmakingSettingsUpdater : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private MatchmakingSettings _settings;

        public void UpdateLookingForPrivateGame(bool b)
        {
            _settings.LookForPrivateGames = b;
        }

        public void UpdateLookingForStartedGames(bool b)
        {
            _settings.LookForStartedGames = b;
        }

        public void UpdatePrivateGameName(string name)
        {
            _settings.PrivateGameName = name;
        }
    }
}
