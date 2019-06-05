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

        // START

        private void Start()
        {
            Debug.LogError("[OWNERSHIP] Start : " + GetComponent<BoltEntity>().NetworkId);
        }

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
                Team = evnt.Team.ToTeam();
                OwnerKartRoot = SWExtensions.KartExtensions.GetKartWithID(OwnerID);

                OnOwnershipSet.Invoke(OwnerKartRoot);
                Debug.LogError("[OWNERSHIP] ItemThrown event received : "+ GetComponent<BoltEntity>().NetworkId);
            }
        }
    }
}
