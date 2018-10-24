using UnityEngine;
using Photon.Lobby;
using Bolt;
using Multiplayer;
using Multiplayer.Teams;

namespace Network
{
    public class KartNetworkBehaviour : EntityBehaviour<IKartState>
    {
        [SerializeField] private PlayerSettingsSO _playerSettings;

        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
            }
        }

        // BOLT

        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);
            state.SetAnimator(GetComponentInChildren<Animator>());
            state.Team = _playerSettings.Team;
            state.Nickname = _playerSettings.Nickname;

            state.AddCallback("Team", ColorChanged);
            state.AddCallback("Nickname", NameChanged);

            ColorChanged();
            NameChanged();
            var lobby = GameObject.Find("LobbyManager");
            if(lobby) lobby.SetActive(false);
        }

        // PUBLIC

        public void DestroyKart()
        {
            if (entity.isOwner)
            {
                Debug.Log("Destroying kart");
                FindObjectOfType<CameraUtils.SpectatorControls>().Enabled = true;
                FindObjectOfType<CameraUtils.CameraPlayerSwitch>().SetCameraToRandomPlayer();
                BoltNetwork.Destroy(gameObject);
            }
        }

        // PRIVATE

        private void ColorChanged()
        {
            GetComponent<PlayerSettings>().Team = TeamsColors.GetTeamFromColor(state.Team);
        }

        private void NameChanged()
        {
            GetComponent<PlayerSettings>().Nickname = state.Nickname;
        }
    }
}
