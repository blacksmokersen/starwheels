using UnityEngine;
using Items;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Controls
{
    public class GodModInputs : BaseKartComponent
    {
        public bool Enabled = true;

        private int ActualItemIndex = 0;

        public void Update()
        {
            if (!enabled) return;

            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ResetLives();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SwitchToNextItem();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    LoseOneLife();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    DestroyKart();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    //SpawnRedKart();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    //SpawnBlueKart();
                }
            }
        }

        public void SpawnRedKart()
        {
            var kart = PhotonNetwork.InstantiateSceneObject("Kart", (transform.position + Vector3.forward), Quaternion.identity);
            kart.GetComponent<PhotonView>().Owner.SetTeam(PunTeams.Team.red);
        }

        public void SpawnBlueKart()
        {
            var kart = PhotonNetwork.InstantiateSceneObject("Kart", (transform.position + Vector3.forward), Quaternion.identity);
            kart.GetComponent<PhotonView>().Owner.SetTeam(PunTeams.Team.blue);
        }

        public void SwitchToNextItem()
        {
            var kartInventory = kartHub.kartInventory;
            var items = ItemsLottery.Items;
            var itemIndex = (ActualItemIndex++) % items.Length;
            kartInventory.Item = items[itemIndex];
            kartEvents.OnItemUsed(kartInventory.Item, kartInventory.Count);
            kartHub.kartInventory.Count = 1000;
        }

        public void LoseOneLife()
        {
            kartHub.kartHealthSystem.HealthLoss();
        }

        public void DestroyKart()
        {
            GetComponent<PhotonView>().RPC("RPCDestroy", RpcTarget.AllBuffered);
        }

        public void ResetLives()
        {
            kartHub.kartHealthSystem.ResetLives();
        }

        [PunRPC]
        private void RPCDestroy()
        {
            kartHub.kartEvents.OnKartDestroyed();
        }
    }
}
