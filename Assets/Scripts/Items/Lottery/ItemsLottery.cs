using UnityEngine;

namespace Items.Lottery
{
    public class ItemsLottery
    {
        public static float LotteryDuration = 3.0f;
        public static Item[] Items;

        public static float ComputeItemChances(ItemBoxSettings settings)
        {
            float total = 0;

            foreach (var item in settings.ItemsChances)
            {
                total += item.Chances;
            }

            return total;
        }

        public static Item GetRandomItem(ItemBoxSettings settings)
        {
            var chancesCount = 0f;
            var randomChance = Random.Range(0, ComputeItemChances(settings));

            foreach (var item in settings.ItemsChances)
            {
                chancesCount += item.Chances;
                if (chancesCount > randomChance)
                {
                    return item.Item;
                }
            }

            return null;
        }
    }
}
