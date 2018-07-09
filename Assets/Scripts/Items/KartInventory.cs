using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items {
    public class KartInventory : MonoBehaviour {

        public enum Directions { Foward, Backward }

        public IItem InventoryItem;
        public IItem StackedItem;

        public Transform FrontItemPosition;
        public Transform BackItemPosition;

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
                if (StackedItem.Count > 0)
                {
                    if (direction == Directions.Foward)
                    {
                        StackedItem.UseForward();
                        // INSTANTIATION 
                    }
                    else if (direction == Directions.Backward)
                    {
                        StackedItem.UseBackward();
                        // INSTANTIATION
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