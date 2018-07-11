using UnityEngine;
using System.Collections.Generic;

namespace Items
{
    public class ItemsLottery : MonoBehaviour
    {
        public static ItemsLottery Instance { get; private set; }
        private List<ItemData> ItemsData;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                ItemsData = new List<ItemData>();
                ItemsData.Add(ScriptableObject.CreateInstance<DiskData>());
                ItemsData.Add(ScriptableObject.CreateInstance<RocketData>());
                ItemsData.Add(ScriptableObject.CreateInstance<NitroData>());
            }
        }
        
        public ItemData PickRandomItem()
        {
            return ItemsData[Random.Range(0, ItemsData.Count - 1)];
        }

        private void OnApplicationQuit()
        {
            Destroy(gameObject);
        }
    }
}
