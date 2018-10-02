using UnityEngine.UI;
using UnityEngine;
using Items;
using Kart;

namespace HUD
{
    public class GameHUD : MonoBehaviour
    {
        [Header("Players")]
        public Text PlayerList;

        [Header("Item")]
        [SerializeField] private Text _itemCountText;
        [SerializeField] private Image _itemTexture;
        [SerializeField] private Image _itemFrame;

        [Header("Speedmeter")]
        [SerializeField] private Image _speedBar;
        [SerializeField] private Text _textSpeed;

        // CORE

        private void Awake()
        {
            UpdateItem(null);
            UpdateItemCount(0);
            UpdatePlayerList();
        }

        // PUBLIC

        public void ObserveKart(GameObject kartRoot)
        {
            /*var kartInventory = kartRoot.GetComponentInChildren<KartInventory>();
            UpdateItem(kartInventory.Item);
            UpdateItemCount(kartInventory.Count);*/
        }

        public void UpdatePlayerList()
        {
            PlayerList.text = null;
            /*
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PlayerList.text += player.NickName + "\t Score: " + player.GetScore() + "\n";
            }
            */
        }

        // PRIVATE

        private void UpdateItem(ItemData item)
        {
            if (item == null)
            {
                _itemTexture.sprite = null;
                _itemTexture.enabled = false;
                _itemFrame.color = Color.white;
                _itemFrame.enabled = false;
            }
            else
            {
                _itemTexture.sprite = item.InventoryTexture;
                _itemTexture.enabled = true;
                _itemFrame.color = item.ItemColor;
                _itemFrame.enabled = true;
            }
        }

        private void UpdateItemCount(int count)
        {
            if (count == 0)
            {
                _itemCountText.text = "";
            }
            else
            {
                _itemCountText.text = "" + count;
            }
        }
    }
}
