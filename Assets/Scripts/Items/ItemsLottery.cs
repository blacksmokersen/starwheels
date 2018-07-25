using UnityEngine;
using System;

namespace Items
{
    public class ItemsLottery
    {
        public static float LOTTERY_DURATION = 3.0f;
        public static ItemData[] Items = Resources.Load<ItemListData>("ItemList").Items;


        public static ItemData GetRandomItem()
        {
            return Items[UnityEngine.Random.Range(0, Items.Length)];
        }
    }
}
