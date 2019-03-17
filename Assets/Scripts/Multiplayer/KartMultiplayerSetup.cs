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
                state.Team = _playerSettings.ColorSettings.BoltColor;
                state.Nickname = _playerSettings.Nickname;
                state.OwnerID = SWMatchmaking.GetMyBoltId();

                PlayerReady playerReadyEvent = PlayerReady.Create();
                playerReadyEvent.Team = state.Team;
                playerReadyEvent.Send();
            }

            GetComponentInChildren<Camera>().enabled = entity.isOwner;
        }

        // PRIVATE

        private void ColorChanged()
        {
            if (entity.isOwner)
            {
                _playerSettings.ColorSettings.BoltColor = state.Team;
            }

            GetComponent<Player>().Team = state.Team.GetTeam();
            var panel = GetComponentInChildren<Common.HUD.NicknamePanel>();
            if (panel)
            {
                panel.SetFrameRendererColor(state.Team);
            }
        }

        private void NameChanged()
        {
            GetComponent<Player>().Nickname = state.Nickname;
            var panel = GetComponentInChildren<Common.HUD.NicknamePanel>();
            if(panel) panel.SetName(state.Nickname);
        }
    }
}
