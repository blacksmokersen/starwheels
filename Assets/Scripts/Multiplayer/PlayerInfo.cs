using UnityEngine;
using Bolt;

namespace Multiplayer
{
    public class PlayerInfo : GlobalEventListener
    {
        public static PlayerInfo Me;

        private string _nickName;
        public string Nickname
        {
            get { return _nickName; }
            set { _nickName = value; OnNicknameChanged.Invoke(_nickName); }
        }

        private Team _team;
        public Team Team
        {
            get { return _team; }
            set { _team = value; OnTeamChanged.Invoke(_team); }
        }

        public int OwnerID;

        [Header("Events")]
        public StringEvent OnNicknameChanged;
        public TeamEvent OnTeamChanged;
        public IntEvent OnOwnerIDChanged;

        private BoltEntity _kartEntity;

        // CORE

        private void Awake()
        {
            _kartEntity = GetComponent<BoltEntity>();
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!evnt.FromSelf)
            {
                if (evnt.Entity == _kartEntity) // This is the new spawned kart
                {
                    Nickname = evnt.Nickname;
                    Team = evnt.Team.ToTeam();
                    OwnerID = evnt.PlayerID;
                }
                else if (evnt.Entity != _kartEntity && _kartEntity.IsOwner) // This is my kart, I send my info to the new player
                {
                    PlayerInfoEvent playerInfoEvent = PlayerInfoEvent.Create(); // We target the new player
                    playerInfoEvent.TargetPlayerID = evnt.PlayerID;
                    playerInfoEvent.Nickname = Nickname;
                    playerInfoEvent.Team = (int)Team;
                    playerInfoEvent.PlayerID = OwnerID;
                    playerInfoEvent.KartEntity = _kartEntity;
                    playerInfoEvent.Send();
                }
            }
        }

        public override void OnEvent(PlayerInfoEvent evnt)
        {
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId() && // This event is for me
                evnt.KartEntity == _kartEntity && // This is the targetted kart
                !evnt.KartEntity.IsOwner) // I don't own this kart
            {
                Nickname = evnt.Nickname;
                Team = evnt.Team.ToTeam();
                OwnerID = evnt.PlayerID;
            }
        }
    }
}


