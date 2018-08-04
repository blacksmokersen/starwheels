using UnityEngine;

namespace Items
{
    public class ItemsLottery
    {
        public static float LotteryDuration = 3.0f;
        public static ItemData[] Items = Resources.Load<ItemListData>("ItemList").Items;
        
        public static ItemData GetRandomItem()
        {
            return Items[Random.Range(0, Items.Length)];
        }
    }
}
