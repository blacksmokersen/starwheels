using UnityEngine;
using Multiplayer;

namespace Items
{
    public class Ownership : MonoBehaviour
    {
        public GameObject OwnerKartRoot;
        public Team Team;

        public void Set(PlayerSettings player)
        {
            OwnerKartRoot = player.gameObject;
            Team = player.Team;
        }

        public bool IsMe(GameObject kartRoot)
        {
            return OwnerKartRoot == kartRoot;
        }

        public bool IsMe(PlayerSettings otherPlayer)
        {
            return otherPlayer.gameObject == OwnerKartRoot;
        }

        public bool IsSameTeam(GameObject kartRoot)
        {
            return kartRoot.GetComponent<PlayerSettings>().Team == Team;
        }

        public bool IsSameTeam(PlayerSettings otherPlayer)
        {
            return otherPlayer.Team == Team;
        }

        public bool IsNotSameTeam(PlayerSettings otherPlayer)
        {
            return otherPlayer.Team != Team;
        }
    }
}
