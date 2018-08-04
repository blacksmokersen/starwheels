using UnityEngine;
using System.Collections;
using HUD;

namespace Items {
    public enum Directions { Default, Forward, Backward, Left, Right }

    public class KartInventory : BaseKartComponent
    {
        public ItemData Item;
        public int Count;
        public ItemPositions ItemPositions;

        private float lotteryTimer = 0f;
        private bool lotteryStarted = false;
        private bool shortenLottery = false;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnCollisionEnterItemBox += () => StartCoroutine(GetLotteryItem());
        }

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
                    FindObjectOfType<HUDUpdater>().UpdateItem(Item,Count);
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
            FindObjectOfType<HUDUpdater>().UpdateItem(Item,Count);
        }

        public IEnumerator GetLotteryItem()
        {
            if (Item != null || lotteryStarted) yield break;
            lotteryStarted = true;
            var lotteryIndex = 0;
            var items = ItemsLottery.Items;
            while (lotteryTimer < ItemsLottery.LOTTERY_DURATION)
            {
                FindObjectOfType<HUDUpdater>().UpdateItem(items[(lotteryIndex++)%items.Length],0);
                yield return new WaitForSeconds(0.1f);
                lotteryTimer+=0.1f;
            }
            Item = ItemsLottery.GetRandomItem();
            Count = Item.Count;
            UpdateHUD();
            ResetLottery();
        }

        private void ResetLottery()
        {
            lotteryTimer = 0f;
            lotteryStarted = false;
            shortenLottery = false;
        }
    }
}