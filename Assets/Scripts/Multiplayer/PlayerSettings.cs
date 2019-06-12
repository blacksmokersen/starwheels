using UnityEngine;
using UnityEngine.Events;
using Multiplayer.Teams;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Multiplayer Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Information")]
        public TeamColorSettings ColorSettings;
        public string Nickname;
        public int ConnectionID;
        public int KartIndex;
        public int AbilityIndex;
        public float InvicibilityOnSpawnDuration;

        [Header("Events")]
        public UnityEvent OnTeamColorUpdated;
        public UnityEvent OnNicknameUpdated;
        public UnityEvent OnAbilityIndexUpdated;
    }
}
