using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Items
{
    public class ItemHUD : MonoBehaviour
    {
        [Header("Item")]
        [SerializeField] private TextMeshProUGUI _itemCountText;
        [SerializeField] private Image _itemTexture;

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
            UpdateItem(kartInventory.CurrentItem);
            UpdateItemCount(kartInventory.CurrentItemCount);

            kartInventory.OnItemGet.AddListener(UpdateItem);
            kartInventory.OnItemUse.AddListener(UpdateItem);
            kartInventory.OnItemCountChange.AddListener(UpdateItemCount);

            Debug.Log("Added listeners");
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
    }
}
