using UnityEngine.UI;
using UnityEngine;
using Items;
using Kart;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

namespace HUD
{
    public class GameHUD : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Text ItemCountText;
        [SerializeField] private Text PlayerList;
        [SerializeField] private Image ItemTexture;
        [SerializeField] private Image ItemFrame;

        // CORE

        private void Start()
        {
            KartEvents.Instance.OnItemGet += UpdateItem;
            KartEvents.Instance.OnItemCountChange += UpdateItemCount;
            KartEvents.Instance.OnScoreChange += UpdatePlayerList;

            UpdateItem(null);
            UpdateItemCount(0);
            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayerList();
        }

        // PUBLIC

        // PRIVATE

        private void UpdateItem(ItemData item)
        {
            if (item == null)
            {
                ItemTexture.sprite = null;
                ItemTexture.enabled = false;
                ItemFrame.color = Color.white;
                ItemFrame.enabled = false;
            }
            else
            {
                ItemTexture.sprite = item.InventoryTexture;
                ItemTexture.enabled = true;
                ItemFrame.color = item.ItemColor;
                ItemFrame.enabled = true;
            }
        }

        private void UpdateItemCount(int count)
        {
            if (count == 0)
            {
                ItemCountText.text = "";
            }
            else
            {
                ItemCountText.text = "" + count;
            }
        }

        private void UpdatePlayerList()
        {
            PlayerList.text = null;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PlayerList.text += player.NickName + "\t Score: " + player.GetScore() + "\n";
            }
        }
    }
}
