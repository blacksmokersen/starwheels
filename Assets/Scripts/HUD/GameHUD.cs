using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Items;
using Kart;

namespace HUD
{
    public class GameHUD : MonoBehaviour
    {
        public Text ItemCountText;
        public Text PlayerList;
        public Image ItemTexture;
        public Image ItemFrame;

        private void Start()
        {
            KartEvents.Instance.OnItemUsed += UpdateItem;
            KartEvents.Instance.OnScoreChange += UpdatePlayerList;

            UpdateItem(null, 0);
            UpdatePlayerList();
        }

        public void UpdateItem(ItemData item, int count)
        {
            ItemTexture.sprite = item != null ? item.InventoryTexture : null;
            ItemFrame.color = item != null ? item.ItemColor : Color.white ;
            ItemCountText.text = "" + count;
        }

        public void UpdatePlayerList()
        {
            PlayerList.text = null;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                PlayerList.text += player.NickName + "\t Score: " + player.GetScore() + "\n";
            }
        }

        void OnPhotonPlayerConnected()
        {
            UpdatePlayerList();
        }
        
        void OnPhotonPlayerDisconnected()
        {
            UpdatePlayerList();
        }
    }
}