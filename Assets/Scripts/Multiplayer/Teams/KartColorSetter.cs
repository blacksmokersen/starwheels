using UnityEngine;
using Bolt;
using SW.Customization;

namespace Multiplayer.Teams
{
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
            state.AddCallback("Team", SetKartTeamColor);
        }

        // PUBLIC

        public void SetKartTeamColor()
        {
            var newColor = _teamsSettings.GetSettings(state.Team.ToTeam()).KartColor;
            var renderer = _kartSetter.CurrentKart.GetComponent<KartSkinSettings>().TargetRenderer;
            renderer.material.SetColor("_BaseColor", newColor);
        }
    }
}
