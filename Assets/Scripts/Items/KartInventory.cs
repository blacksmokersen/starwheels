using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Items
{
    public class KartInventory : BaseKartComponent
    {
        private static ItemListData itemsList;

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

            photonView = GetComponent<PhotonView>();
            itemsList = Resources.Load<ItemListData>("Data/ItemList");

            kartEvents.OnItemBoxGet += () =>
            {
                if (photonView.IsMine || !PhotonNetwork.IsConnected)
                    StartItemLottery();
            };
        }

        // PUBLIC

        public void StartItemLottery()
        {
            StartCoroutine(GetLotteryItem());
        }

        public bool IsEmpty()
        {
            return Item == null || Count == 0;
        }

        public void ItemAction(Direction direction, float aimAxis)
        {
            if (_lotteryStarted && !_shortenLottery && _lotteryTimer > 1f)
            {
                _lotteryTimer = ItemsLottery.LotteryDuration;
                _shortenLottery = true;
            }
            else
            {
                UseStack(direction,aimAxis);
            }
        }

        public void SetItem(ItemData item, int count)
        {
            int itemIndex = itemsList.GetItemDataIndex(item);
            photonView.RPC("RPCSetItem", RpcTarget.AllBufferedViaServer, itemIndex, count);
        }

        public void SetItem(ItemData item)
        {
            int itemIndex = itemsList.GetItemDataIndex(item);
            photonView.RPC("RPCSetItem", RpcTarget.AllBufferedViaServer, itemIndex, item.Count);
        }

        public void SetCount(int count)
        {
            int itemIndex = itemsList.GetItemDataIndex(Item);
            photonView.RPC("RPCSetItem", RpcTarget.AllBufferedViaServer, itemIndex, count);
        }

        // PRIVATE

        private void UseStack(Direction direction,float aimAxis)
        {
            if (IsEmpty()) return;

            ItemBehaviour itemObj;
            var obj = PhotonNetwork.Instantiate("Items/" + Item.ItemPrefab.name, new Vector3(0, -10, 0), Quaternion.identity, 0);
            itemObj = obj.GetComponent<ItemBehaviour>();
            itemObj.Spawn(this, direction, aimAxis);

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

        [PunRPC]
        private void RPCSetItem(int itemDataIndex, int count)
        {
            Item = itemDataIndex == -1 ? null : itemsList.Items[itemDataIndex];
            if(kartEvents.OnItemGet != null) kartEvents.OnItemGet(Item);

            Count = count;
            if(kartEvents.OnItemCountChange != null) kartEvents.OnItemCountChange(Count);
        }
    }
}
