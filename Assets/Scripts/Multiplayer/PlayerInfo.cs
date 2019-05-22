using UnityEngine;
using UnityEngine.Events;
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
            if (evnt.Entity == GetComponent<BoltEntity>())
            {
                Debug.LogError("Hello ... : " + SWMatchmaking.GetMyBoltId());
                Nickname = evnt.Nickname;
                Team = evnt.Team.ToTeam();
                OwnerID = evnt.PlayerID;
            }

            if (evnt.PlayerID != SWMatchmaking.GetMyBoltId()) // I should no receive my own event
            {
                Debug.LogError("... world !: " + SWMatchmaking.GetMyBoltId());
                PlayerInfoEvent playerInfoEvent = PlayerInfoEvent.Create(GlobalTargets.Others);
                playerInfoEvent.TargetPlayerID = evnt.PlayerID;
                playerInfoEvent.Nickname = Nickname;
                playerInfoEvent.Team = (int)Team;
                playerInfoEvent.PlayerID = OwnerID;
                playerInfoEvent.KartEntity = KartEntity;
                playerInfoEvent.Send();
            }
        }

        public override void OnEvent(PlayerInfoEvent evnt)
        {
            if (evnt.TargetPlayerID == SWMatchmaking.GetMyBoltId() && evnt.KartEntity == KartEntity)
            {
                Debug.LogError("Received: " + SWMatchmaking.GetMyBoltId());
                Nickname = evnt.Nickname;
                Team = evnt.Team.ToTeam();
                OwnerID = evnt.PlayerID;
            }
        }
    }
}


