using UnityEngine;
using Kart;
using System.Collections;

namespace Items {
    public class KartInventory : MonoBehaviour
    {        
        [SerializeField]
        private ItemTypes _inventoryItem = ItemTypes.None;
        public ItemTypes InventoryItem
        {
            get
            {
                return _inventoryItem;
            }
            set
            {
                if (_inventoryItem == ItemTypes.None || value == ItemTypes.None)
                    _inventoryItem = value;
            }
        }

        [SerializeField]
        private ItemTypes _stackedItem = ItemTypes.None;
        public ItemTypes StackedItem
        {
            get
            {
                return _stackedItem;
            }
            set
            {
                if (_stackedItem == ItemTypes.None || value == ItemTypes.None)
                    _stackedItem = value;
            }
        }

        public int Count;

        public Transform FrontItemPosition; // Where to instantiate if item thrown forwards
        public Transform BackItemPosition; // Where to instantiate if item thrown backwards

        public GameObject MinePrefab;
        public GameObject DiskPrefab;
        public GameObject RocketPrefab;

        private float lotteryTimer = 0f;

        public void ItemAction(Directions direction)
        {
            if (InventoryItem != ItemTypes.None && StackedItem == ItemTypes.None)
            {
                StackedItem = InventoryItem;
                InventoryItem = ItemTypes.None;
                Count = 3; // To change
            }
            else if (StackedItem != ItemTypes.None)
            {
                UseStack(direction);
            }
        }

        public void UseStack(Directions direction)
        {
            if (StackedItem != ItemTypes.None)
            {
                if (Count > 0)
                {
                    UseItem(StackedItem, direction);
                    Count--;
                }
                if(Count == 0)
                {
                    StackedItem = ItemTypes.None;
                }
            }
        }

        public void UseItem(ItemTypes item, Directions direction)
        {
            switch (item)
            {
                case ItemTypes.Disk:
                    var disk = Instantiate(DiskPrefab, FrontItemPosition.position, Quaternion.identity);
                    disk.GetComponent<DiskBehaviour>().SetDirection(transform.forward);
                    break;
                case ItemTypes.Rocket:
                    var rocket = Instantiate(RocketPrefab, FrontItemPosition.position, Quaternion.identity);
                    rocket.GetComponent<RocketBehaviour>().SetDirection(transform.forward);
                    break;
                case ItemTypes.Mine:
                    Instantiate(MinePrefab, BackItemPosition.position, Quaternion.identity);
                    break;
                case ItemTypes.Nitro:
                    StartCoroutine(GetComponentInParent<KartPhysics>().Boost(2f,10f,500f));
                    break;
            }
        }

        public IEnumerator GetLotteryItem()
        {
            var lottery = FindObjectOfType<ItemsLottery>();
            while(lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                lotteryTimer += Time.deltaTime;
                yield return null;
            }
            InventoryItem = lottery.PickRandomItemType();
            lotteryTimer = 0f;
        }
    }
}