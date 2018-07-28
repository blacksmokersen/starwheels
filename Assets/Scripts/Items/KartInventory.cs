using UnityEngine;
using Kart;
using System.Collections;
using HUD;

namespace Items {
    public enum Directions { Forward, Backward  }

    public class KartInventory : MonoBehaviour
    {
        public ItemData Item;

        public int Count;
        public ItemPositions ItemPositions;

        private float lotteryTimer = 0f;
        private bool lotteryStarted = false;
        private bool shortenLottery = false;

        public void ItemAction(Directions direction)
        {
            if(lotteryStarted && !shortenLottery && lotteryTimer > 1f)
            {
                lotteryTimer += 0.5f;
                shortenLottery = true;
            }
            else
            {
                UseStack(direction);
            }
            UpdateHUD();
        }

        public void UseStack(Directions direction)
        {
            if (Item != null)
            {
                if (Count > 0)
                {
                    UseItem(Item, direction);
                    Count--;
                    FindObjectOfType<HUDUpdater>().UpdateItemCount(Count);
                    if (Count == 0)
                    {
                        Item = null;
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
            
            itemObj.Spawn(this,direction);
        }

        public void UpdateHUD()
        {
            FindObjectOfType<HUDUpdater>().SetItem(Item);
        }

        public IEnumerator GetLotteryItem()
        {
            if (Item != null || lotteryStarted) yield break;
            lotteryStarted = true;
            var lotteryIndex = 0;
            var items = ItemsLottery.Items;
            Debug.Log(items);
            while (lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                FindObjectOfType<HUDUpdater>().SetItem(items[(lotteryIndex++)%items.Length]);
                lotteryTimer += Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
                lotteryTimer+=0.1f;
            }
            Item = ItemsLottery.GetRandomItem();
            Count = Item.Count;
            UpdateHUD();
            lotteryTimer = 0f;
            lotteryStarted = false;
            shortenLottery = false;
        }
    }
}