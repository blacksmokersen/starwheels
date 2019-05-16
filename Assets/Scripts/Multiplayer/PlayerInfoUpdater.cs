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
            if (evnt.Entity == GetComponent<BoltEntity>())
            {
                _playerInfo.Nickname = evnt.Nickname;
                _playerInfo.Team = evnt.Team.ToTeam();
            }
        }
    }
}
