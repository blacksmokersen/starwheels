using UnityEngine;

namespace SW.Matchmaking
{
    [CreateAssetMenu(menuName = "Matchmaking Settings")]
    public class MatchmakingSettings : ScriptableObject
    {
        [Header("Private Games")]
        public bool LookForPrivateGames;
        public string PrivateGameName;

        [Header("Public Games")]
        public bool LookForStartedGames;
    }
}
