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
        public BoltEntity KartEntity;

        [Header("Events")]
        public StringEvent OnNicknameChanged;
        public TeamEvent OnTeamChanged;
        public IntEvent OnOwnerIDChanged;

        public void Start()
        {
            if (KartEntity.isOwner)
            {
                Me = this;
            }
        }

        // BOLT

        public override void OnEvent(PlayerReady evnt)
        {
            if (!evnt.Entity.isOwner)
            {
                if (evnt.Entity == GetComponent<BoltEntity>()) // This is the new spawned kart
                {
                    Nickname = evnt.Nickname;
                    Team = evnt.Team.ToTeam();
                    OwnerID = evnt.PlayerID;
                }
                else if (evnt.Entity != GetComponent<BoltEntity>() && KartEntity.isOwner) // This is my kart, I send my info to the new player
                {
                    PlayerInfoEvent playerInfoEvent = PlayerInfoEvent.Create(evnt.RaisedBy); // We target the new player
                    playerInfoEvent.TargetPlayerID = evnt.PlayerID;
                    playerInfoEvent.Nickname = Nickname;
                    playerInfoEvent.Team = (int)Team;
                    playerInfoEvent.PlayerID = OwnerID;
                    playerInfoEvent.KartEntity = KartEntity;
                    playerInfoEvent.Send();
                }
            }
        }

        public override void OnEvent(PlayerInfoEvent evnt)
        {
            if (evnt.KartEntity == KartEntity && !evnt.KartEntity.isOwner)
            {
                Nickname = evnt.Nickname;
                Team = evnt.Team.ToTeam();
                OwnerID = evnt.PlayerID;
            }
        }
    }
}


