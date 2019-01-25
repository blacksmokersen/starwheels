using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Multiplayer
{
    public class GameReadyEventListener : GlobalEventListener
    {
        [Header("Unity Events")]
        public UnityEvent OnGameReady;

        // BOLT

        public override void OnEvent(GameReady evnt)
        {
            OnGameReady.Invoke();
        }

        public override void OnEvent(LobbyCountdown evnt)
        {
            Debug.Log("Starts in " + evnt.Time);
        }
    }
}
