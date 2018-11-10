using UnityEngine;
using Multiplayer;
using Multiplayer.Teams;
using Bolt;

namespace Common.HUD
{
    public class VisibleForTeammates : EntityBehaviour<IKartState>
    {
        [Header("Materials")]
        [SerializeField] private Renderer _targetRenderer;
        [SerializeField] private Material _alwaysVisibleMaterial;
        [SerializeField] private Material _defaultMaterial;

        private PlayerSettings _playerSettings;

        // CORE

        private void Awake()
        {
            _playerSettings = Resources.Load<PlayerSettings>(Constants.Resources.PlayerSettings);

            if (!_defaultMaterial) _defaultMaterial = _targetRenderer.material;
        }

        // BOLT

        public override void Attached()
        {
            var myTeam = TeamsColors.GetTeamFromColor(_playerSettings.Team);
            var thisKartTeam = TeamsColors.GetTeamFromColor(state.Team);

            if (myTeam == thisKartTeam)
            {
                SetAlwaysVisble();
            }
        }

        // PRIVATE

        private void SetAlwaysVisble()
        {
            _targetRenderer.material = _alwaysVisibleMaterial;
        }

        private void ResetToDefault()
        {
            _targetRenderer.material = _defaultMaterial;
        }
    }
}
