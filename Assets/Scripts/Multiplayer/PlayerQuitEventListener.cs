using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class PlayerQuitEventListener : GlobalEventListener
    {
        [Header("Events")]
        public IntEvent OnPlayerQuit;

        public override void Disconnected(BoltConnection connection)
        {
            if (BoltNetwork.IsServer)
            {
                PlayerQuit playerQuitEvent = PlayerQuit.Create();
                playerQuitEvent.PlayerID = (int)connection.ConnectionId;
                playerQuitEvent.Send();
                Debug.Log("A player has left the game. Sent PlayerQuit event.");
            }
        }

        public override void OnEvent(PlayerQuit evnt)
        {
            if (OnPlayerQuit != null)
            {
                Debug.LogError("A player has left the game. Sent PlayerQuit event.");
                OnPlayerQuit.Invoke(evnt.PlayerID);
            }
        }
    }
}
