using UnityEngine;
using Kart;
using System.Collections;
using HUD;

namespace Items {
    public class KartInventory : MonoBehaviour
    {
        public ItemData CurrentItem;
        public ItemData Item;

        public int Count;

        public ItemPositions ItemPositions;
        public GameObject MinePrefab;
        public GameObject DiskPrefab;
        public GameObject RocketPrefab;

        private float lotteryTimer = 0f;

        public void ItemAction(Directions direction)
        {
            if (CurrentItem == null)
            {
                if (Item != null)
                {
                    CurrentItem = Item;
                    Item = null;
                    Count = 3;
                }
            }
            else
            {
                UseStack(direction);
            }
            UpdateHUD();
        }

        public void UseStack(Directions direction)
        {
            if (CurrentItem != null)
            {
                if (Count > 0)
                {
                    UseItem(CurrentItem, direction);
                    Count--;
                }
                else if(Count == 0)
                {
                    CurrentItem = null;
                }
            }
            UpdateHUD();
        }

        public void UseItem(ItemData item, Directions direction)
        {
            var itemObj = Instantiate(item.ItemPrefab);
            itemObj.SetOwner(this);
        }

        public void UpdateHUD()
        {
            FindObjectOfType<HUDUpdater>().SetItem(CurrentItem, Item);
        }

        public IEnumerator GetLotteryItem()
        {
            if (Item != null) yield break;

            var lottery = FindObjectOfType<ItemsLottery>();
            while(lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                lotteryTimer += Time.deltaTime;
                yield return null;
            }
            Item = lottery.GetRandomItem();
            UpdateHUD();
            lotteryTimer = 0f;
        }
    }
}