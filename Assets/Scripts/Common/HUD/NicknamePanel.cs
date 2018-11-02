#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using TMPro;
using Multiplayer.Teams;
using Bolt;

namespace Common.HUD
{
    public class NicknamePanel : EntityBehaviour<IKartState>
    {
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SpriteRenderer frameRenderer;

        // CORE

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        // BOLT

        public override void Attached()
        {
            SetName(state.Nickname);
            SetFrameRendererColor(state.Team);
        }

        public override void ControlGained()
        {
            gameObject.SetActive(false);
        }

        public override void ControlLost()
        {
            gameObject.SetActive(true);
        }

        // PUBLIC

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetFrameRendererColor(Color color)
        {
            frameRenderer.color = color;
        }

        public void SetFrameRendererTeam(Team team)
        {
            SetFrameRendererColor(TeamsColors.GetColorFromTeam(team));
        }

        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(NicknamePanel))]
    public class NicknamePanelEditor : Editor
    {
        Color frameColor;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NicknamePanel nicknamePanel = (NicknamePanel)target;
            if (GUILayout.Button("Show Panel"))
                nicknamePanel.ShowPanel();
            if (GUILayout.Button("Hide Panel"))
                nicknamePanel.HidePanel();

            frameColor = EditorGUILayout.ColorField("Change color", frameColor);
            nicknamePanel.SetFrameRendererColor(frameColor);
        }
    }
    #endif
}
