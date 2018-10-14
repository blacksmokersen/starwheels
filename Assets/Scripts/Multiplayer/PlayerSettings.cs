using UnityEngine;
using Bolt;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Multiplayer
{
    public class PlayerSettings : EntityBehaviour
    {
        #region Variables
        public static PlayerSettings Me;

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

        [Header("Events")]
        public StringEvent OnNicknameChanged;
        public TeamEvent OnTeamChanged;
        public IntEvent OnScoreChanged;

        public override void Attached()
        {
            if (entity.isOwner)
                Me = this;
        }
    }

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(PlayerSettings))]
    public class PlayerSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PlayerSettings playerSettings = (PlayerSettings)target;
            playerSettings.Nickname = EditorGUILayout.TextField("Enter nickname", playerSettings.Nickname);
            playerSettings.Team = (Team)EditorGUILayout.EnumPopup("Choose a team", playerSettings.Team);
        }
    }
#endif
    #endregion
}


