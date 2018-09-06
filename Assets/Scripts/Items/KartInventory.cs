using UnityEngine;
using System.Collections;
using Photon.Pun;

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

        private float _lotteryTimer = 0f;
        private bool _lotteryStarted = false;
        private bool _shortenLottery = false;

        // CORE

        private new void Awake()
        {
            base.Awake();

            kartEvents.OnGetItemBox += () => {
                if (photonView.IsMine || !PhotonNetwork.IsConnected)
                    StartCoroutine(GetLotteryItem());
            };
        }

        // PUBLIC

        public bool IsEmpty()
        {
            return Item == null;
        }

        public void ItemAction(Direction direction)
        {
            if (_lotteryStarted && !_shortenLottery && _lotteryTimer > 1f)
            {
                _lotteryTimer += ShorteningSeconds;
                _shortenLottery = true;
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

            kartEvents.OnItemUsed(Item, Count);
        }

        public void UseItem(ItemData item, Direction direction)
        {
            ItemBehaviour itemObj;
            var obj = PhotonNetwork.Instantiate(item.ItemPrefab.name, new Vector3(0, -10, 0), Quaternion.identity, 0);
            itemObj = obj.GetComponent<ItemBehaviour>();
            itemObj.Spawn(this,direction);
        }


        public IEnumerator GetLotteryItem()
        {
            if (Item != null || _lotteryStarted) yield break;

            _lotteryStarted = true;
            var lotteryIndex = 0;
            var items = ItemsLottery.Items;

            while (_lotteryTimer < ItemsLottery.LotteryDuration)
            {
                kartEvents.OnItemUsed(items[(lotteryIndex++) % items.Length], 0);
                yield return new WaitForSeconds(0.1f);
                _lotteryTimer += 0.1f;
            }

            Item = ItemsLottery.GetRandomItem();
            Count = Item.Count;

            kartEvents.OnItemUsed(Item, Count);
            kartEvents.OnLotteryStop();

            ResetLottery();
        }

        // PRIVATE

        private void ResetLottery()
        {
            _lotteryTimer = 0f;
            _lotteryStarted = false;
            _shortenLottery = false;
        }
    }
}
