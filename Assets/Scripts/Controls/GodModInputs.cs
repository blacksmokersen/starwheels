using UnityEngine;
using Items;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace Controls
{
    public class GodModInputs : BaseKartComponent
    {
        private bool _enabled = true;
        private int _currentItemIndex = 0;

        // CORE

        private void Update()
        {
            if (!_enabled) return;

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

        // PUBLIC

        // PRIVATE

        private void SetUnlimitedItems()
        {
            kartHub.kartInventory.SetCount(1000);
        }

        public void SwitchToNextItem()
        {
            var items = ItemsLottery.Items;

            kartHub.kartInventory.SetItem(items[(_currentItemIndex++) % items.Length]);
            SetUnlimitedItems();
        }

        private void LoseOneLife()
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
