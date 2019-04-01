using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Multiplayer
{
    public class GameReadyEventListener : GlobalEventListener
    {
        [Header("Unity Events")]
        public UnityEvent OnGameReady;

        [Header("Game Started")]
        [SerializeField] private BoolVariable _gameStartedVariable;

        // CORE

        private void Start()
        {
            if (_gameStartedVariable.Value)
            {
                if (OnGameReady != null)
                {
                    OnGameReady.Invoke();
                }
            }
        }

        // BOLT

        public override void OnEvent(GameReady evnt)
        {
            if (OnGameReady != null)
            {
                OnGameReady.Invoke();
            }
            _gameStartedVariable.Value = true;
        }

        public override void OnEvent(LobbyCountdown evnt)
        {
            //Debug.Log("Starts in " + evnt.Time);
        }
    }
}
