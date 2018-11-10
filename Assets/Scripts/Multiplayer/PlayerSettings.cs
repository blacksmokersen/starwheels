using UnityEngine;

namespace Multiplayer
{
    [CreateAssetMenu(fileName = "Multiplayer/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Player Information")]
        public Color Team;
        public string Nickname;
        public int ConnectionID;
        public int KartIndex;
        public int AbilityIndex;

        // CORE

        // PLAYER RELATED EVENTS

        public void SendKartDestroyedEvent()
        {
            KartDestroyed kartDestroyedEvent = KartDestroyed.Create();
            kartDestroyedEvent.Team = Team;
            kartDestroyedEvent.ConnectionID = ConnectionID;
            kartDestroyedEvent.Send();
        }
    }
}
