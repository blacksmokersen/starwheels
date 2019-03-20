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
            state.AddCallback("Team", TeamChanged);
        }

        // PUBLIC

        public void SetAlwaysVisble()
        {
            _targetRenderer.material = _alwaysVisibleMaterial;
        }

        public void ResetToDefault()
        {
            _targetRenderer.material = _defaultMaterial;
        }

        // PRIVATE

        private void TeamChanged()
        {
            var myTeam = _playerSettings.ColorSettings.TeamEnum;
            if (myTeam == state.Team.ToTeam())
            {
                SetAlwaysVisble();
            }
        }
    }
}
