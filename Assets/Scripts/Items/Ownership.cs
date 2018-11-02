using UnityEngine;
using Multiplayer;

namespace Items
{
    public class Ownership : MonoBehaviour
    {
        public GameObject OwnerKartRoot;
        public Team Team;

        public void Set(Player player)
        {
            OwnerKartRoot = player.gameObject;
            Team = player.Team;
        }

        public bool IsMe(GameObject kartRoot)
        {
            return OwnerKartRoot == kartRoot;
        }

        public bool IsMe(Player otherPlayer)
        {
            return otherPlayer.gameObject == OwnerKartRoot;
        }

        public bool IsSameTeam(GameObject kartRoot)
        {
            return kartRoot.GetComponent<Player>().Team == Team;
        }

        public bool IsSameTeam(Player otherPlayer)
        {
            return otherPlayer.Team == Team;
        }

        public bool IsNotSameTeam(Player otherPlayer)
        {
            return otherPlayer.Team != Team;
        }
    }
}
