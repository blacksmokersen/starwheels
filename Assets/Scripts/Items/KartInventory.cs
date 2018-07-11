using UnityEngine;

namespace Items {
    public class KartInventory : MonoBehaviour
    {        
        private ItemData _inventoryItem;
        public ItemData InventoryItem
        {
            get
            {
                return _inventoryItem;
            }
            set
            {
                if (_inventoryItem != null)
                    _inventoryItem = value;
            }
        }
        public ItemData StackedItem;

        public Transform FrontItemPosition; // Where to instantiate if item thrown forwards
        public Transform BackItemPosition; // Where to instantiate if item thrown backwards

        public void ItemAction(Directions direction)
        {
            if(InventoryItem != null && StackedItem == null)
            {
                StackedItem = InventoryItem;
                InventoryItem = null;
            }
            else if (StackedItem != null)
            {
                UseStack(direction);
            }
        }

        public void UseStack(Directions direction)
        {
            if (StackedItem != null)
            {
                var builder = ItemsBuilders.Instance.GetBuilder(StackedItem.Type);
                if (StackedItem.Count > 0)
                {
                    if (direction == Directions.Foward)
                    {
                        builder.UseForward();
                    }
                    else if (direction == Directions.Backward)
                    {
                        builder.UseBackward();
                    }
                }
                if(StackedItem.Count == 0)
                {
                    StackedItem = null;
                }
            }
        }
    }
}