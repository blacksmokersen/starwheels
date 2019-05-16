using UnityEngine;
using Bolt;
using Multiplayer;

namespace Network
{
    public class KartMultiplayerSetup : EntityBehaviour<IKartState>
    {
        [Header("Temporal Anti-Aliasing")]
        [SerializeField] private Transform _kartMeshesRootTransform;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private float _delayBeforeDestroyKart;

        [SerializeField] private Animator _kartAnimator;
        [SerializeField] private Animator _CharacterAnimator;

        private void Awake()
        {
            if (!BoltNetwork.IsConnected)
            {
                BoltLauncher.StartServer();
            }
        }

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform, _kartMeshesRootTransform);
            state.SetAnimator(_kartAnimator);
            state.AddAnimator(_CharacterAnimator);

            state.AddCallback("Team", ColorChanged);

            var backCam = GetComponentInChildren<Camera>();
            if (backCam != null)
            {
                backCam.enabled = entity.isOwner;
            }
        }

        // PRIVATE

        private void ColorChanged()
        {
            GetComponent<PlayerInfo>().Team = state.Team.ToTeam();
        }
    }
}
