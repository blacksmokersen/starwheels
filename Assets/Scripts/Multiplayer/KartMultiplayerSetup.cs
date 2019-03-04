using UnityEngine;
using Photon.Lobby;
using Bolt;
using Multiplayer;
using Multiplayer.Teams;
using System.Collections;

namespace Network
{
    public class KartMultiplayerSetup : EntityBehaviour<IKartState>
    {
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
            state.SetTransforms(state.Transform, transform);
            state.SetAnimator(GetComponentInChildren<Animator>());
            state.AddCallback("Team", ColorChanged);
            state.AddCallback("Nickname", NameChanged);

            if (entity.isOwner)
            {
                //state.Team = _playerSettings.TeamColor;
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
                _playerSettings.TeamColor = state.Team;
                Debug.Log("New team color : " + _playerSettings.TeamColor);
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
