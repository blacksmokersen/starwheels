using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Items;
using Kart;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

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

        private KartEvents _kartEvent;

        private void Start()
        {
            KartEvents.Instance.OnScoreChange += UpdatePlayerList;

            UpdateItem(null, 0);
            UpdatePlayerList();
        }

        public void ObserveKart(GameObject kartRoot)
        {
            _kartEvent = kartRoot.GetComponent<KartEvents>();
            _kartEvent.OnItemUsed += UpdateItem;
            _kartEvent.OnVelocityChange += UpdateSpeedmeter;
        }

        public void UpdateItem(ItemData item, int count)
        {
            _itemTexture.sprite = item != null ? item.InventoryTexture : null;
            _itemFrame.color = item != null ? item.ItemColor : Color.white ;
            _itemCountText.text = "" + count;
        }

        public void UpdatePlayerList()
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
