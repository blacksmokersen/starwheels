using UnityEngine;
using Bolt;

namespace Multiplayer
{
    [RequireComponent(typeof(PlayerInfo))]
    public class PlayerInfoUpdater : GlobalEventListener
    {
        private PlayerInfo _playerInfo;

        // CORE

        private void Awake()
        {
            _playerInfo = GetComponent<PlayerInfo>();
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {

        }
    }
}
