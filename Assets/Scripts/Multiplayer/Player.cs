using UnityEngine;
using Bolt;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Multiplayer
{
    public class Player : EntityBehaviour
    {
        public static Player Me;

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

        [Header("Events")]
        public StringEvent OnNicknameChanged;
        public TeamEvent OnTeamChanged;
        public IntEvent OnScoreChanged;

        // CORE

        // BOLT

        public override void Attached()
        {
            if (entity.isOwner)
            {
                Me = this;
            }
        }
    }
}


