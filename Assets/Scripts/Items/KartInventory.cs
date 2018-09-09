using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Items
{
    public class KartInventory : BaseKartComponent
    {
        [Header("Inventory")]
        [SerializeField] public ItemData Item;
        [SerializeField] public int Count;
        [SerializeField] public ItemPositions ItemPositions;

        private float _lotteryTimer = 0f;
        private bool _lotteryStarted = false;
        private bool _shortenLottery = false;

        // CORE

        private new void Awake()
        {
            base.Awake();

            kartEvents.OnItemBoxGet += () => {
                if (photonView.IsMine || !PhotonNetwork.IsConnected)
                    StartCoroutine(GetLotteryItem());
            };
        }

        // PUBLIC

        public bool IsEmpty()
        {
            return Item == null || Count == 0;
        }

        public void ItemAction(Direction direction)
        {
            if (_lotteryStarted && !_shortenLottery && _lotteryTimer > 1f)
            {
                _lotteryTimer = ItemsLottery.LotteryDuration;
                _shortenLottery = true;
            }
            else
            {
                UseStack(direction);
            }
        }

        public void SetItem(ItemData item, int count)
        {
            Item = item;
            Count = count;

            kartEvents.OnItemGet(Item);
            kartEvents.OnItemCountChange(Count);
        }

        public void SetItem(ItemData item)
        {
            SetItem(item, item.Count);
        }

        public void SetCount(int count)
        {
            SetItem(Item, count);
        }

        // PRIVATE

        private void UseStack(Direction direction)
        {
            if (IsEmpty()) return;

            ItemBehaviour itemObj;
            var obj = PhotonNetwork.Instantiate("Items/" + Item.ItemPrefab.name, new Vector3(0, -10, 0), Quaternion.identity, 0);
            itemObj = obj.GetComponent<ItemBehaviour>();
            itemObj.Spawn(this, direction);

            if (--Count == 0)
            {
                Item = null;
                kartEvents.OnItemGet(null);
            }

            kartEvents.OnItemUse(Item);
            kartEvents.OnItemCountChange(Count);
        }

        private IEnumerator GetLotteryItem()
        {
            if (!IsEmpty() || _lotteryStarted) yield break;

            _lotteryStarted = true;
            var lotteryIndex = 0;
            var items = ItemsLottery.Items;

            while (_lotteryTimer < ItemsLottery.LotteryDuration)
            {
                kartEvents.OnItemGet(items[(lotteryIndex++) % items.Length]);
                yield return new WaitForSeconds(0.1f);
                _lotteryTimer += 0.1f;
            }

            Item = ItemsLottery.GetRandomItem();
            SetItem(Item, Item.Count);

            kartEvents.OnLotteryStop();

            ResetLottery();
        }

        private void ResetLottery()
        {
            _lotteryTimer = 0f;
            _lotteryStarted = false;
            _shortenLottery = false;
        }
    }
}
