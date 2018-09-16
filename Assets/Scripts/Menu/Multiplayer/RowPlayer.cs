using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class RowPlayer : MonoBehaviour
    {
        [SerializeField] private Text playerNameText;
        [SerializeField] private Image backgroundColor;

        private Player _player;

        // CORE

        // PUBLIC

        public int GetPlayerId()
        {
            return _player.ActorNumber;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
            SetName(player.NickName);
            SetTeam(player.GetTeam());

            if (PhotonNetwork.LocalPlayer == player)
            {
                playerNameText.color = Color.yellow;
            }
        }

        public void SetName(string name)
        {
            playerNameText.text = name;
        }

        public void SetTeam(PunTeams.Team team)
        {
            backgroundColor.color = GetTeamColor(team);
        }

        // PRIVATE

        private Color GetTeamColor(PunTeams.Team team)
        {
            switch (team)
            {
                case PunTeams.Team.blue: return new Color(0.5f, 0.5f, 1f, 0.3f);
                case PunTeams.Team.red: return new Color(1f, 0.5f, 0.5f, 0.3f);
            }

            return new Color(1f, 1f, 1f, 0.3f);
        }
    }
}
