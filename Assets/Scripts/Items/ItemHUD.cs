using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Items
{
    public class ItemHUD : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField] private TextMeshProUGUI _itemCountText;
        [SerializeField] private Image _itemTexture;

        [Header("Initializing State")]
        [SerializeField] private Image _initializingSprite;
        [SerializeField] private Image _loadingGauge;

        // CORE

        private void Awake()
        {
            UpdateItem(null);
            UpdateItemCount(0);
        }

        // PUBLIC

        public void ObserveKart(GameObject kartRoot)
        {
            var kartInventory = kartRoot.GetComponentInChildren<Inventory>();
            if (kartInventory)
            {
                UpdateItem(kartInventory.CurrentItem);
                UpdateItemCount(kartInventory.CurrentItemCount);

                kartInventory.OnItemGet.AddListener(UpdateItem);
                kartInventory.OnItemUse.AddListener(UpdateItem);
                kartInventory.OnItemCountChange.AddListener(UpdateItemCount);
            }

            var kartLottery = kartRoot.GetComponentInChildren<Lottery>();
            if (kartLottery)
            {
                kartLottery.OnLotteryStart.AddListener(ShowInitializingState);
                kartLottery.OnLotteryUpdate.AddListener(UpdateLoadingGauge);
                kartLottery.OnLotteryStop.AddListener(() => UpdateLoadingGauge(0f));
                kartLottery.OnLotteryStop.AddListener(HideInitializingState);
            }
        }

        public void UpdateItem(Item item)
        {
            if (item == null)
            {
                _itemTexture.sprite = null;
                _itemTexture.enabled = false;
            }
            else
            {
                _itemTexture.sprite = item.InventoryTexture;
                _itemTexture.enabled = true;
            }
        }

        public void UpdateItemCount(int count)
        {
            _itemCountText.text = "" + count;
        }

        // PRIVATE

        private void ShowInitializingState()
        {
            _initializingSprite.enabled = true;
        }

        private void HideInitializingState()
        {
            _initializingSprite.enabled = false;
        }

        private void UpdateLoadingGauge(float percentage)
        {
            _loadingGauge.fillAmount = percentage;
        }
    }
}
