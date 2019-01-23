using UnityEngine;
using UnityEngine.Events;
using Common.PhysicsUtils;
using Bolt;

namespace Items
{
    public class ItemMerger : EntityBehaviour<IKartState>, IControllable
    {
        [Tooltip("Seconds with button pressed before merging the item")]
        [SerializeField] private FloatVariable _secondsBeforeMerging;

        [Header("Item Source")]
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Lottery.Lottery _lottery;

        [Header("Boost")]
        [SerializeField] private Boost _boost;
        [SerializeField] private BoostSettings _boostSettings;

        [Header("Unity Events")]
        public UnityEvent OnItemMerging;

        private float _timer = 0f;

        // CORE

        private void Update()
        {
            if(entity.isAttached && entity.isOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButton(Constants.Input.UseItem) || Input.GetButton(Constants.Input.UseItemBackward) || Input.GetButton(Constants.Input.UseItemForward))
            {
                _timer += Time.deltaTime;

                if (_timer > _secondsBeforeMerging.Value)
                {
                    MergeItem();
                }
            }
            if (Input.GetButtonUp(Constants.Input.UseItem) || Input.GetButtonUp(Constants.Input.UseItemBackward) || Input.GetButtonUp(Constants.Input.UseItemForward))
            {
                _timer = 0f;
            }
        }

        // PRIVATE

        private void MergeItem()
        {
            if (_inventory.CurrentItem != null)
            {
                var numberOfCharge = _inventory.CurrentItemCount / _inventory.CurrentItem.Count;
                _boost.CustomBoostFromBoostSettings(_boostSettings);
                _inventory.SetItem(null, 0);

                if (OnItemMerging != null)
                {
                    OnItemMerging.Invoke();
                }
            }
            else if (_lottery.LotteryStarted)
            {
                _lottery.StopAllCoroutines();
                _lottery.ResetLottery();
                _boost.CustomBoostFromBoostSettings(_boostSettings);

                if (OnItemMerging != null)
                {
                    OnItemMerging.Invoke();
                }
            }
        }
    }
}
