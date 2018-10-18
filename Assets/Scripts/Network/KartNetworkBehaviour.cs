using UnityEngine;
using Photon.Lobby;
using Bolt;
using Multiplayer;
using Multiplayer.Teams;

namespace Network
{
    public class KartNetworkBehaviour : EntityBehaviour<IKartState>
    {
        private void Awake()
        {
            if (!BoltNetwork.isConnected)
            {
                BoltLauncher.StartServer();
            }
        }

        public override void Attached()
        {
            if (entity.isOwner)
            {
                state.SetTransforms(state.Transform, transform);
                state.SetAnimator(GetComponentInChildren<Animator>());
                state.Team = LobbyPhotonPlayer.localPlayer.playerColor;
                state.Nickname = LobbyPhotonPlayer.localPlayer.playerName;

                state.AddCallback("Team", ColorChanged);
                state.AddCallback("Nickname", NameChanged);

                ColorChanged();
                NameChanged();
                GameObject.Find("LobbyManager").SetActive(false);
            }
            GameObject.Find("LobbyManager").SetActive(false);
        }

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
