using UnityEngine;
using TMPro;
using Bolt;

namespace Common.HUD
{
    public class NicknamePanel : EntityBehaviour<IKartState>
    {
        [Header("Elements")]
        [SerializeField] private TextMeshPro nameText;
        [SerializeField] private SpriteRenderer frameRenderer;

        private GameSettings _gameSettings;

        // CORE

        private void Awake()
        {
            _gameSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings);
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("Nickname", NicknameChanged);
            state.AddCallback("Team", TeamChanged);
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
            var color = _gameSettings.TeamsListSettings.GetSettings(team).NameplateColor;
            SetFrameRendererColor(color);
        }

        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);
        }

        // PRIVATE

        private void NicknameChanged()
        {
            SetName(state.Nickname);
        }

        private void TeamChanged()
        {
            SetFrameRendererTeam(state.Team.ToTeam());
        }
    }
}
