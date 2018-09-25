using UnityEngine;

namespace Items
{
    [CreateAssetMenu]
    public class ItemListData : ScriptableObject
    {
        public ItemData[] Items;

        public int GetItemDataIndex(ItemData itemData)
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
    }
}
