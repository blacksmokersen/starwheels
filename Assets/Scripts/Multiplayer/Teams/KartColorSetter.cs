using UnityEngine;
using Bolt;
using SW.Customization;

namespace Multiplayer.Teams
{
    [DisallowMultipleComponent]
    public class KartColorSetter : EntityBehaviour<IKartState>
    {
        [Header("Renderer")]
        [SerializeField] private KartMeshSetter _kartSetter;

        private TeamsListSettings _teamsSettings;

        // CORE

        private void Awake()
        {
            _teamsSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings).TeamsListSettings;
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("Team", SetKartTeamColorAccordingToState);
        }

        // PUBLIC

        public void SetKartTeamColorAccordingToState()
        {
            if (entity.IsAttached)
            {
                SetKartTeamColor(state.Team.ToTeam());
            }
            else
            {
                Debug.LogError("Can't set team color according to state if entity is not attached.");
            }
        }

        public void SetKartTeamColor(Team team)
        {
            if (_teamsSettings != null && team != Team.None)
            {
                var newColor = _teamsSettings.GetSettings(team).KartColor;
                var renderer = _kartSetter.CurrentKart.GetComponent<KartSkinSettings>().TargetRenderer;
                renderer.material.SetColor("_Color", newColor);
            }
            else
            {
                Debug.LogError("Team = None or TeamSettings was null. Couldn't set color.");
            }
        }

        public void SetKartColor(string hex)
        {
            var color = new Color();
            ColorUtility.TryParseHtmlString(hex, out color);
            var renderer = _kartSetter.CurrentKart.GetComponent<KartSkinSettings>().TargetRenderer;
            renderer.material.SetColor("_Color", color);
        }
    }
}
