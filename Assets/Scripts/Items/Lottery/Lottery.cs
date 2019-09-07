using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items.Lottery
{
    public class Lottery : EntityBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Events")]
        public UnityEvent OnLotteryStart;
        public UnityEvent OnLotteryStop;
        public FloatEvent OnLotteryUpdate;

        [SerializeField] private Inventory _inventory;

        [HideInInspector] public bool LotteryStarted = false;
        private float _lotteryTimer = 0f;
        private bool _shortenLottery = false;

        // CORE

        private void Update()
        {
            if (entity.isAttached && entity.isControllerOrOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if(Enabled && Input.GetButtonDown(Constants.Input.UseItem) && LotteryStarted && _lotteryTimer > 2f)
            {
                //_shortenLottery = true;
            }
        }

        public void ResetLottery()
        {
            _lotteryTimer = 0f;
            LotteryStarted = false;
            _shortenLottery = false;
            StopAllCoroutines();

            OnLotteryStop.Invoke();
        }

        // PRIVATE

        private IEnumerator GetLotteryItem(ItemBoxSettings settings)
        {
            if (LotteryStarted || !_inventory.IsEmpty()) yield break;
            LotteryStarted = true;
            OnLotteryStart.Invoke();

            while (_lotteryTimer < ItemsLottery.LotteryDuration && _shortenLottery == false)
            {
                yield return new WaitForSeconds(0.1f);
                _lotteryTimer += 0.1f;
                OnLotteryUpdate.Invoke(_lotteryTimer / ItemsLottery.LotteryDuration);
            }

            var Item = ItemsLottery.GetRandomItem(settings);
            _inventory.SetItem(Item, Item.Count);

            OnLotteryStop.Invoke();
            ResetLottery();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.ItemBox) && _inventory.Enabled && _inventory.CurrentItem == null)
            {
                if (entity.isAttached && entity.isControllerOrOwner)
                {
                    var itemsChancesSettings = other.GetComponent<ItemBox>().CurrentSettings;
                    this.StartCoroutine(GetLotteryItem(itemsChancesSettings));
                }

                other.GetComponent<ItemBox>().Activate();
            }
        }
    }
}
