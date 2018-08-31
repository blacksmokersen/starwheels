using UnityEngine;
using System.Collections;

namespace Items
{
    public class KartInventory : BaseKartComponent
    {
        [Header("Inventory")]
        public ItemData Item;
        public int Count;
        public ItemPositions ItemPositions;

        [Header("Lottery")]
        public float ShorteningSeconds;

        private float lotteryTimer = 0f;
        private bool lotteryStarted = false;
        private bool shortenLottery = false;

        private new void Awake()
        {
            base.Awake();
            kartEvents.OnCollisionEnterItemBox += () => {
                if (photonView.isMine || !PhotonNetwork.connected)
                    StartCoroutine(GetLotteryItem());
            };
        }

        public void ItemAction(Direction direction)
        {
            if(lotteryStarted && !shortenLottery && lotteryTimer > 1f)
            {
                lotteryTimer += ShorteningSeconds;
                shortenLottery = true;
            }
            else
            {
                UseStack(direction);
            }
        }

        public void UseStack(Direction direction)
        {
            if (Item != null)
            {
                if (Count > 0)
                {
                    UseItem(Item, direction);
                    Count--;
                    if (Count == 0)
                    {
                        Item = null;
                    }
                }
            }
            kartEvents.OnItemUsed(Item,Count);
        }

        public void UseItem(ItemData item, Direction direction)
        {
            ItemBehaviour itemObj;
            var obj = PhotonNetwork.Instantiate(item.ItemPrefab.name, new Vector3(1500,1500,1500), Quaternion.identity, 0);
            itemObj = obj.GetComponent<ItemBehaviour>();
            itemObj.Spawn(this,direction);
        }


        public IEnumerator GetLotteryItem()
        {
            if (Item != null || lotteryStarted) yield break;
            lotteryStarted = true;
            var lotteryIndex = 0;
            var items = ItemsLottery.Items;
            while (lotteryTimer < ItemsLottery.LotteryDuration)
            {
                kartEvents.OnItemUsed(items[(lotteryIndex++)%items.Length],0);
                yield return new WaitForSeconds(0.1f);
                lotteryTimer+=0.1f;
            }
            Item = ItemsLottery.GetRandomItem();
            Count = Item.Count;
            kartEvents.OnItemUsed(Item, Count);
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
