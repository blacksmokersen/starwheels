using UnityEngine;
using Multiplayer;

namespace Items
{
    public class Ownership : MonoBehaviour
    {
        public GameObject OwnerKartRoot;
        public Team Team;

        [Header("Events")]
        public GameObjectEvent OnOwnershipSet;

        public void Set(PlayerInfo player)
        {
            Team = player.Team;
            OwnerKartRoot = player.gameObject;
            OnOwnershipSet.Invoke(OwnerKartRoot);
        }
    }
}
