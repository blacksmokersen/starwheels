using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Items
{
    [DisallowMultipleComponent]
    public class ItemHUD : MonoBehaviour, IObserver
    {
        [Header("Item HUD")]
        [SerializeField] private TextMeshProUGUI _itemCountText;
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private Image _itemTexture;

        [Header("Shield HUD")]
        [SerializeField] private Image _shieldImage;
        //[SerializeField] private GameObject _fullBoostImage;
        //[SerializeField] private GameObject _lowBoostImage;

        [Header("Initializing State")]
        [SerializeField] private Image _initializingSprite;
        [SerializeField] private Image _itemLoadingGauge;

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
            UpdateLoadingGauge(0);
            HideInitializingState();
            //UpdateMergeLogo(0);
            //HideBoostLogo();

            var kartInventory = kartRoot.GetComponentInChildren<Inventory>();
            if (kartInventory)
            {
                UpdateItem(kartInventory.CurrentItem);
                UpdateItemCount(kartInventory.CurrentItemCount);

                kartInventory.OnItemGet.AddListener(UpdateItem);
                kartInventory.OnItemUse.AddListener(UpdateItem);
                kartInventory.OnItemCountChange.AddListener(UpdateItemCount);
                //kartInventory.OnItemCountChange.AddListener(UpdateMergeLogo);
            }

            var kartLottery = kartRoot.GetComponentInChildren<Lottery.Lottery>();
            if (kartLottery)
            {
                kartLottery.OnLotteryStart.AddListener(ShowInitializingState);
                //kartLottery.OnLotteryStart.AddListener(ShowBoostLogo);
                kartLottery.OnLotteryUpdate.AddListener(UpdateLoadingGauge);
                kartLottery.OnLotteryStop.AddListener(() => UpdateLoadingGauge(0f));
                kartLottery.OnLotteryStop.AddListener(HideInitializingState);
                //kartLottery.OnLotteryStop.AddListener(HideBoostLogo);
            }

            var kartShield = kartRoot.GetComponentInChildren<Health.ShieldEffects>();
            if (kartShield)
            {
                kartShield.OnCooldownUpdated.AddListener(UpdateShieldMask);
                UpdateShieldMask(1f);
            }
        }

        public void UpdateItem(Item item)
        {
            if (item == null)
            {
                _itemTexture.sprite = null;
                _itemName.text = null;
                _itemTexture.enabled = false;
                _currentItem = null;
            }
            else
            {
                _itemTexture.sprite = item.InventoryTexture;
                _itemName.text = item.Name;
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
            _itemLoadingGauge.fillAmount = percentage;
        }

        private void UpdateShieldMask(float percentage)
        {
            _shieldImage.fillAmount = percentage;
        }

        /*
        private void ShowBoostLogo()
        {
            _fullBoostImage.SetActive(true);
        }

        private void HideBoostLogo()
        {
            _fullBoostImage.SetActive(false);
        }

        private void UpdateMergeLogo(int count)
        {
            if (count <= 0)
            {
                _shieldImage.SetActive(false);
            }
            else
            {
                _shieldImage.SetActive(true);
            }
        }
        */
    }
}
