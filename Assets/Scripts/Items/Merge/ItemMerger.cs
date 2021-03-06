﻿using UnityEngine;
using Common.PhysicsUtils;
using Bolt;

namespace Items.Merge
{
    [DisallowMultipleComponent]
    public class ItemMerger : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _canMerge = value;
                _enabled = value;
            }
        }

        [Header("Settings")]
        [Tooltip("Seconds with button pressed before merging the item")]
        [SerializeField] private FloatVariable _secondsBeforeMerging;

        [Header("Shield")]
        [SerializeField] private Health.ShieldEffects _shield;

        [Header("Item Source")]
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Lottery.Lottery _lottery;

        [Header("Bonuses References")]
        [SerializeField] private Boost _boost;
        [SerializeField] private BoostSettings _boostSettings;

        private float _timer = 0f;
        private bool _canMerge = true;

        private enum MergeMode { Shield, SpeedBoost };

        // CORE

        private void Update()
        {
            if(entity.IsAttached && entity.IsOwner)
            {
                MapInputs();
            }
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Enabled)
            {
                if (Input.GetButton(Constants.Input.MergeItem) && _shield.CanBeUsed)
                {
                    GrantBoosts(MergeMode.Shield);
                }
            }

            /* KEEP IT JUST IN CASE
            if (Enabled)
            {
                if (Input.GetButton(Constants.Input.MergeItem))
                {
                    _timer += Time.deltaTime;

                    if (_timer > _secondsBeforeMerging.Value && _canMerge)
                    {
                        ConsumeItem();
                        _timer = 0f;
                        _canMerge = false;
                    }
                }
                if (Input.GetButtonUp(Constants.Input.MergeItem))
                {
                    _timer = 0f;
                    _canMerge = true;
                }
            }
            */
        }

        // PRIVATE

        /* KEEP IT JUST IN CASE
        private void ConsumeItem()
        {
            if (_inventory.CurrentItem != null)
            {
                _inventory.SetCount(_inventory.CurrentItemCount - 1);
                GrantBoosts(MergeMode.Shield);
            }
            else if (_lottery.LotteryStarted)
            {
                _lottery.StopAllCoroutines();
                _lottery.ResetLottery();
                GrantBoosts(MergeMode.SpeedBoost);
            }
        }
        */

        private void GrantBoosts(MergeMode mode)
        {
            ItemMerging itemMergingEvent = ItemMerging.Create();
            itemMergingEvent.Entity = entity;
            switch (mode)
            {
                case MergeMode.Shield:
                    itemMergingEvent.Shield = true;
                    break;
                case MergeMode.SpeedBoost:
                    itemMergingEvent.Shield = false;
                    _boost.CustomBoostFromBoostSettings(_boostSettings);
                    break;
            }
            itemMergingEvent.Send();
        }
    }
}
