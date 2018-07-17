using UnityEngine;
using Kart;
using System.Collections;
using HUD;

namespace Items {
    public class KartInventory : MonoBehaviour
    {
        public ItemData StackedItem;
        public ItemData InventoryItem;

        public int Count;
        public ItemPositions ItemPositions;

        private float lotteryTimer = 0f;
        private bool lotteryStarted = false;
        private bool shortenLottery = false;

        private void Update()
        {
            Debug.Log("Timer : " + lotteryTimer);
        }

        public void ItemAction(Directions direction)
        {
            if(lotteryStarted && !shortenLottery && lotteryTimer > 1f)
            {
                lotteryTimer += 0.5f;
                shortenLottery = true;
            }
            else if (StackedItem == null)
            {
                if (InventoryItem != null)
                {
                    StackedItem = InventoryItem;
                    Count = InventoryItem.Count;
                    InventoryItem = null;
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
            if (StackedItem != null)
            {
                if (Count > 0)
                {
                    UseItem(StackedItem, direction);
                    Count--;
                }
                else if(Count == 0)
                {
                    StackedItem = null;
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
            FindObjectOfType<HUDUpdater>().SetItem(StackedItem, InventoryItem);
        }

        public IEnumerator GetLotteryItem()
        {
            if (InventoryItem != null) yield break;
            lotteryStarted = true;
            var lottery = FindObjectOfType<ItemsLottery>();
            while(lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                lotteryTimer += Time.deltaTime;
                yield return null;
            }
            InventoryItem = lottery.GetRandomItem();
            UpdateHUD();
            lotteryTimer = 0f;
            lotteryStarted = false;
            shortenLottery = false;
        }
    }
}