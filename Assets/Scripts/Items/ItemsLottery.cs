using UnityEngine;

namespace Items
{
    public class ItemsLottery
    {
        public static float LotteryDuration = 3.0f;
        public static ItemData[] Items;
        public static float TotalItemChances = ComputeItemChances();

        public static float ComputeItemChances()
        {
            if (Items == null)
                Items = Resources.Load<ItemListData>("ItemList").Items;
            
            float total = 0;
            foreach (var item in Items)
            {
                total += item.Chances;
            }
            return total;
        }

        public static ItemData GetRandomItem()
        {
            if (Items == null)
                Items = Resources.Load<ItemListData>("ItemList").Items;

            var chancesCount = 0f;
            var randomChance = Random.Range(0, TotalItemChances);
            foreach (var item in Items)
            {
                chancesCount += item.Chances;
                if (chancesCount > randomChance)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
