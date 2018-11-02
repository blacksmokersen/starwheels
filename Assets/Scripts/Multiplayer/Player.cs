using UnityEngine;
using Bolt;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Multiplayer
{
    public class Player : EntityBehaviour
    {
        #region Variables
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

        private int _score;
        public int Score
        {
            get { return _score; }
            set { _score = value; OnScoreChanged.Invoke(_score); }
        }

        public BoltConnection Connection;
        #endregion

        [SerializeField] private PlayerSettings playerSettingsSO;

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

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Player))]
    public class PlayerSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Player playerSettings = (Player)target;
            playerSettings.Nickname = EditorGUILayout.TextField("Enter nickname", playerSettings.Nickname);
            playerSettings.Team = (Team)EditorGUILayout.EnumPopup("Choose a team", playerSettings.Team);
        }
    }
#endif
    #endregion
}


