using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
using Multiplayer.Teams;

namespace Common.HUD
{
    public class NicknamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SpriteRenderer frameRenderer;

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
            frameRenderer.color = TeamsColors.GetColorFromTeam(team);
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
