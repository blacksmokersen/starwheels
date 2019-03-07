using UnityEngine;
using UnityEngine.Events;

namespace Multiplayer
{
    [CreateAssetMenu(menuName = "Multiplayer Settings/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Information")]
        public Color TeamColor;
        public string Nickname;
        public int ConnectionID;
        public int KartIndex;
        public int AbilityIndex;

        [Header("Events")]
        public UnityEvent OnTeamColorUpdated;
        public UnityEvent OnNicknameUpdated;
        public UnityEvent OnAbilityIndexUpdated;
    }
}
