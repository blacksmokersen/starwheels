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
        [Header("Players")]
        public Text PlayerList;

        // CORE

        [Header("Item")]
        [SerializeField] private Text _itemCountText;
        [SerializeField] private Image _itemTexture;
        [SerializeField] private Image _itemFrame;

        [Header("Speedmeter")]
        [SerializeField] private Image _speedBar;
        [SerializeField] private Text _textSpeed;

        private KartEvents _kartEvent;

        private void Start()
        {
            KartEvents.Instance.OnItemGet += UpdateItem;
            KartEvents.Instance.OnItemCountChange += UpdateItemCount;
            KartEvents.Instance.OnScoreChange += UpdatePlayerList;

            UpdateItem(null);
            UpdateItemCount(0);
            UpdatePlayerList();
        }

        public void ObserveKart(GameObject kartRoot)
        {
            _kartEvent = kartRoot.GetComponent<KartEvents>();
            _kartEvent.OnItemUse += UpdateItem;
            _kartEvent.OnItemCountChange += UpdateItemCount;
            _kartEvent.OnVelocityChange += UpdateSpeedmeter;

            var kartInventory = kartRoot.GetComponentInChildren<KartInventory>();
            UpdateItem(kartInventory.Item);
            UpdateItemCount(kartInventory.Count);
        }

        // PUBLIC

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

        private void UpdatePlayerList()
        {
            PlayerList.text = null;
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                PlayerList.text += player.NickName + "\t Score: " + player.GetScore() + "\n";
            }
        }

        public void UpdateSpeedmeter(Vector3 kartVelocity)
        {
            var speed = Mathf.Round(kartVelocity.magnitude);
            _speedBar.fillAmount = speed / 70;
            _textSpeed.text = "" + speed * 2;
        }
    }
}
