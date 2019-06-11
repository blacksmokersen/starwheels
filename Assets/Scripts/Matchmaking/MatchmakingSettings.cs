using UnityEngine;

namespace SW.Matchmaking
{
    [CreateAssetMenu(menuName = "Matchmaking Settings")]
    public class MatchmakingSettings : ScriptableObject
    {
        public bool LookForStartedGames;
    }
}
