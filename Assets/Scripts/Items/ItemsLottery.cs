using UnityEngine;

namespace Items
{
    public class ItemsLottery
    {
        public static float LotteryDuration = 3.0f;
        public static ItemData[] Items = Resources.Load<ItemListData>("ItemList").Items;
        public static float TotalItemChances = ComputeItemChances();

        public static float ComputeItemChances()
        {
            float total = 0;
            foreach (var item in Items)
            {
                total += item.Chances;
            }
            return total;
        }

        public static ItemData GetRandomItem()
        {
            var count = 0f;
            var randomChance = Random.Range(0, TotalItemChances);
            foreach (var item in Items)
            {
                count += item.Chances;
                if (count > randomChance)
                {
                    return item;
                }
            }
            return null;
        }
    }
}