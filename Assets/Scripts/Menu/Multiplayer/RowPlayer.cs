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

        // CORE

        // PUBLIC

        public int GetPlayerId()
        {
            return 1;
        }

        public void SetPlayer(Player player)
        {
            SetName(player.Nickname);
            SetTeam(player.Team);
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
