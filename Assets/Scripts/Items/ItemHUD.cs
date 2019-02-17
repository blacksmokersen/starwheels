using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Items
{
    public class ItemHUD : MonoBehaviour, IObserver
    {
        [Header("Item HUD")]
        [SerializeField] private TextMeshProUGUI _itemCountText;
        [SerializeField] private Image _itemTexture;

        [Header("Merge HUD")]
        [SerializeField] private GameObject _fullBoostImage;
        [SerializeField] private GameObject _lowBoostImage;

        [Header("Initializing State")]
        [SerializeField] private Image _initializingSprite;
        [SerializeField] private Image _loadingGauge;

        private Item _currentItem;

        // CORE

        private void Awake()
        {
            UpdateItem(null);
            UpdateItemCount(0);
        }

        // PUBLIC

        public void Observe(GameObject kartRoot)
        {
            var kartInventory = kartRoot.GetComponentInChildren<Inventory>();
            if (kartInventory)
            {
                UpdateItem(kartInventory.CurrentItem);
                UpdateItemCount(kartInventory.CurrentItemCount);

                kartInventory.OnItemGet.AddListener(UpdateItem);
                kartInventory.OnItemUse.AddListener(UpdateItem);
                kartInventory.OnItemCountChange.AddListener(UpdateItemCount);
                kartInventory.OnItemCountChange.AddListener(UpdateMergeLogo);
            }

            var kartLottery = kartRoot.GetComponentInChildren<Lottery.Lottery>();
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
                _currentItem = null;
            }
            else
            {
                _itemTexture.sprite = item.InventoryTexture;
                _itemTexture.enabled = true;
                if (_currentItem == null)
                {
                    _currentItem = item;
                }
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

        private void UpdateMergeLogo(int count)
        {
            if(count == 0)
            {
                _fullBoostImage.SetActive(false);
                _lowBoostImage.SetActive(false);
            }
            else if(count == _currentItem.Count)
            {
                _fullBoostImage.SetActive(true);
                _lowBoostImage.SetActive(false);
            }
            else
            {
                _fullBoostImage.SetActive(false);
                _lowBoostImage.SetActive(true);
            }
        }
    }
}
