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
            state.SetAnimator(GetComponentInChildren<Animator>());

            state.AddCallback("Team", ColorChanged);
            state.AddCallback("Nickname", NameChanged);

            if (entity.isOwner)
            {
                state.Nickname = _playerSettings.Nickname;

                PlayerReady playerReadyEvent = PlayerReady.Create();
                playerReadyEvent.Team = state.Team;
                playerReadyEvent.Send();
            }

            GetComponentInChildren<Camera>().enabled = entity.isOwner;
        }

        // PRIVATE

        private void ColorChanged()
        {
            GetComponent<Player>().Team = state.Team.ToTeam();
        }

        private void NameChanged()
        {
            GetComponent<Player>().Nickname = state.Nickname;
        }
    }
}
