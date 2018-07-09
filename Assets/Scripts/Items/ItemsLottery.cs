using UnityEngine;
using System.Collections.Generic;

namespace Items
{
    public class ItemsLottery : MonoBehaviour
    {
        public static ItemsLottery Instance { get; private set; }
        public List<GameObject> ItemsPrefabs;

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
        
        public GameObject PickRandomItem()
        {
            return ItemsPrefabs[Random.Range(0, ItemsPrefabs.Count - 1)];
        }

        private void OnApplicationQuit()
        {
            Destroy(gameObject);
        }
    }
}
