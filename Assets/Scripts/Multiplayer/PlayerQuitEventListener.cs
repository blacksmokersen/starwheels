using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class PlayerQuitEventListener : GlobalEventListener
    {
        [Header("Events")]
        public IntEvent OnPlayerQuit;
        public IntEvent OnPlayerJoin;

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

        public override void Connected(BoltConnection connection)
        {
            OnPlayerJoin.Invoke((int)connection.ConnectionId);
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
