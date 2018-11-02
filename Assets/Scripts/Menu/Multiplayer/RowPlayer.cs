using UnityEngine;
using UnityEngine.UI;
using Multiplayer;
using Multiplayer.Teams;

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
            return 1;// _player.ActorNumber;
        }

        public void SetPlayer(Player player)
        {
            _player = player;
            SetName(player.Nickname);
            SetTeam(player.Team);

            /*if (PhotonNetwork.LocalPlayer == player)
            {
                playerNameText.color = Color.yellow;
            }
            */
        }

        public void SetName(string name)
        {
            playerNameText.text = name;
        }

        public void SetTeam(Team team)
        {
            backgroundColor.color = TeamsColors.GetColorFromTeam(team);
        }

        // PRIVATE
    }
}
