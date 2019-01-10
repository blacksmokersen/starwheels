using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items
{
    public class Lottery : EntityBehaviour, IControllable
    {
        [Header("Events")]
        public UnityEvent OnLotteryStart;
        public UnityEvent OnLotteryStop;
        public FloatEvent OnLotteryUpdate;

        [SerializeField] private Inventory _inventory;

        private float _lotteryTimer = 0f;
        private bool _shortenLottery = false;
        private bool _lotteryStarted = false;

        // CORE

        private void Update()
        {
            if (entity.isControllerOrOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if(Input.GetButtonDown(Constants.Input.UseItem) && _lotteryStarted && _lotteryTimer > 2f)
            {
                _shortenLottery = true;
            }
        }

        // PRIVATE

        private IEnumerator GetLotteryItem()
        {
            if (_lotteryStarted || !_inventory.IsEmpty()) yield break;
            OnLotteryStart.Invoke();
            _lotteryStarted = true;
            var items = ItemsLottery.Items;

            while (_lotteryTimer < ItemsLottery.LotteryDuration && _shortenLottery == false)
            {
                yield return new WaitForSeconds(0.1f);
                _lotteryTimer += 0.1f;
                OnLotteryUpdate.Invoke(_lotteryTimer / ItemsLottery.LotteryDuration);
            }

            var Item = ItemsLottery.GetRandomItem();
            _inventory.SetItem(Item, Item.Count);

            OnLotteryStop.Invoke();
            ResetLottery();
        }

        private void ResetLottery()
        {
            _lotteryTimer = 0f;
            _lotteryStarted = false;
            _shortenLottery = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.ItemBox) && _inventory.CurrentItem == null)
            {
                if (entity.isControllerOrOwner)
                {
                    this.StartCoroutine(GetLotteryItem());
                }

                other.GetComponent<ItemBox>().Activate();
            }
        }
    }
}
