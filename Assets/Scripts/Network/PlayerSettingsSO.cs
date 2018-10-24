using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(fileName = "Multiplayer/Player Settings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        public Color Team;
        public string Nickname;
        public int ConnectionID;
    }
}
