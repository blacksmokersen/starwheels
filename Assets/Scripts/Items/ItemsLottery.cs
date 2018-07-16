using UnityEngine;
using System;

namespace Items
{
    public class ItemsLottery : MonoBehaviour
    {
        public static ItemsLottery Instance { get; private set; } // Singleton Pattern

        public static float LOTTERY_DURATION = 3.0f;

        public ItemData[] Items;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public ItemData GetRandomItem()
        {
            return Items[UnityEngine.Random.Range(0, Items.Length)];
        }

        public ItemTypes PickRandomItemType()
        {
            var itemTypes = Enum.GetValues(typeof(ItemTypes));
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);            
            return (ItemTypes)itemTypes.GetValue(UnityEngine.Random.Range(1, itemTypes.Length));
        }

        private void OnApplicationQuit()
        {
            Destroy(gameObject);
        }
    }
}
