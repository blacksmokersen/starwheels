using UnityEngine;
using UnityEditor;

namespace Multiplayer
{
    public class PlayerSettings : MonoBehaviour
    {
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
    }

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
}


