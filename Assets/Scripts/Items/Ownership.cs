using UnityEngine;
using Multiplayer;
using Bolt;

namespace Items
{
    public class Ownership : GlobalEventListener
    {
        public string Label;

        [Header("Owner Information")]
        public int OwnerID;
        public string OwnerNickname;
        public GameObject OwnerKartRoot;
        public Team Team;

        [Header("Events")]
        public GameObjectEvent OnOwnershipSet;

        // PUBLIC

        public void Set(PlayerInfo player)
        {
            Team = player.Team;
            OwnerKartRoot = player.gameObject;
            OwnerNickname = player.Nickname;
            OwnerID = player.OwnerID;

            OnOwnershipSet.Invoke(OwnerKartRoot);
        }

        public override void OnEvent(ItemThrown evnt)
        {
            if (evnt.Entity == GetComponent<BoltEntity>())
            {
                Label = evnt.ItemName;
                OwnerID = evnt.OwnerID;
                OwnerNickname = evnt.OwnerNickname;
                OwnerKartRoot = SWExtensions.KartExtensions.GetKartWithID(OwnerID);
                Team = evnt.Team.ToTeam();

                OnOwnershipSet.Invoke(OwnerKartRoot);
            }
        }
    }
}
