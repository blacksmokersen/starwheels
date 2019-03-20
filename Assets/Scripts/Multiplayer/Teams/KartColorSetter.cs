using UnityEngine;
using Bolt;

namespace Multiplayer.Teams
{
    public class KartColorSetter : EntityBehaviour<IKartState>
    {
        [SerializeField] private Renderer targetKartRenderer;

        private TeamsListSettings _teamsSettings;

        // CORE

        private void Awake()
        {
            _teamsSettings = Resources.Load<GameSettings>(Constants.Resources.GameSettings).TeamsListSettings;
        }

        // BOLT

        public override void Attached()
        {
            state.AddCallback("Team", TeamChanged);
        }

        // PRIVATE

        private void TeamChanged()
        {
            Debug.Log("Team changed : " + state.Team);
            var newColor = _teamsSettings.GetSettings(state.Team.ToTeam()).KartColor;
            targetKartRenderer.material.SetColor("_BaseColor", newColor);
        }
    }
}
