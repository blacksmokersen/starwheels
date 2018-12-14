using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [CreateAssetMenu]
    public class ItemListData : ScriptableObject
    {
        public Item[] Items;

        public int GetItemDataIndex(Item itemData)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == itemData)
                {
                    return i;
                }
            }
            return -1;
        }

        public Sprite GetItemIconUsingName(string name)
        {
            foreach(var item in Items)
            {
                if(item.Name == name)
                {
                    return item.InventoryTexture;
                }
            }
            return null;
        }
    }
}
