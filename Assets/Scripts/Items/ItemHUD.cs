using UnityEngine.UI;
using UnityEngine;

namespace Items
{
    public class ItemHUD : MonoBehaviour
    {
        [Header("Item")]
        [SerializeField] private Text _itemCountText;
        [SerializeField] private Image _itemTexture;
        [SerializeField] private Image _itemFrame;

        // CORE

        private void Start()
        {
            _itemFrame = GameObject.Find("ItemFrame").GetComponent<Image>();
            _itemTexture = GameObject.Find("ItemSprite").GetComponent<Image>();
            _itemCountText = GameObject.Find("ItemCount").GetComponent<Text>();

            UpdateItem(null);
            UpdateItemCount(0);
        }

        // PUBLIC

        public void ObserveKart(GameObject kartRoot)
        {
            var kartInventory = kartRoot.GetComponentInChildren<Inventory>();
            UpdateItem(kartInventory.CurrentItem);
            UpdateItemCount(kartInventory.CurrentItemCount);
        }

        // PRIVATE

        private void UpdateItem(Item item)
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
