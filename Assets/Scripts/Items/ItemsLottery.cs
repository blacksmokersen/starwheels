using UnityEngine;
using System;

namespace Items
{
    public class ItemsLottery
    {
        public static float LOTTERY_DURATION = 3.0f;
        public static ItemData[] Items;

        public static ItemData GetRandomItem()
        {
            if (Items == null)
            {
                Items = Resources.Load<ItemListData>("ItemList").Items;
            }

            return Items[UnityEngine.Random.Range(0, Items.Length)];
        }
    }
}
