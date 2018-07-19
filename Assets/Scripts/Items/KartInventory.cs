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
          //  Debug.Log("Timer : " + lotteryTimer);
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
                    if (Count == 0)
                    {
                        StackedItem = null;
                    }
                }
            }
            UpdateHUD();
        }

        public void UseItem(ItemData item, Directions direction)
        {
            ItemBehaviour itemObj;
            if (PhotonNetwork.connected)
            {
                var obj = PhotonNetwork.Instantiate(item.ItemPrefab.name, new Vector3(1500,1500,1500), Quaternion.identity, 0);
                itemObj = obj.GetComponent<ItemBehaviour>();
            }
            else
            {
                itemObj = Instantiate(item.ItemPrefab);
            }
            
            itemObj.SetOwner(this);
        }

        public void UpdateHUD()
        {
            FindObjectOfType<HUDUpdater>().SetItem(StackedItem, InventoryItem);
        }

        public IEnumerator GetLotteryItem()
        {
            if (InventoryItem != null || lotteryStarted) yield break;
            lotteryStarted = true;
            var lottery = FindObjectOfType<ItemsLottery>();
            var lotteryIndex = 0;
            while(lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                Debug.Log(lotteryTimer + " - " + ItemsLottery.LOTTERY_DURATION);
                var items = ItemsLottery.Instance.Items;
                FindObjectOfType<HUDUpdater>().SetItem(StackedItem, items[(lotteryIndex++)%items.Length]);
                lotteryTimer += Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
                lotteryTimer+=0.1f;
            }
            InventoryItem = lottery.GetRandomItem();
            UpdateHUD();
            lotteryTimer = 0f;
            lotteryStarted = false;
            shortenLottery = false;
        }
    }
}