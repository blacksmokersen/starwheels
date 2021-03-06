﻿using UnityEngine;
using Common;
using Multiplayer;
using Bolt;
using ThrowingSystem;
using System.Collections;

namespace Items
{
    public class Inventory : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool CanUseItem = true;

        [Header("Merging")]
        [Tooltip("Seconds with button pressed before merging the item")]
        [SerializeField] private FloatVariable _secondsBeforeMerging;

        [Header("Spam Prevention")]
        [SerializeField] private float _antiSpamDuration;

        [Header("Throwing System")]
        [SerializeField] private ThrowableLauncher _projectileLauncher;
        [SerializeField] private ThrowingDirection _throwingDirection;

        [Header("Current Item")]
        public Item CurrentItem;
        public int CurrentItemCount;

        [Header("Events")]
        public ItemEvent OnItemGet;
        public ItemEvent OnItemUse;
        public IntEvent OnItemCountChange;

        [HideInInspector] public bool IsWallDetected;
        private float _timer = 0f;

        // CORE

        private void Awake()
        {
            CurrentItem = null;
            CurrentItemCount = 0;
        }

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
            if (Enabled)
            {
                if (Input.GetButton(Constants.Input.UseItem) || Input.GetButton(Constants.Input.UseItemBackward) || Input.GetButton(Constants.Input.UseItemForward))
                {
                    _timer += Time.deltaTime;
                }
                if (Input.GetButtonUp(Constants.Input.UseItem) || Input.GetButtonUp(Constants.Input.UseItemBackward) || Input.GetButtonUp(Constants.Input.UseItemForward))
                {
                    if (_timer < _secondsBeforeMerging.Value)
                    {
                        UseItem();
                    }
                    else if (_secondsBeforeMerging.Value == 0)
                    {
                        UseItem();
                    }
                    _timer = 0f;
                }
            }
        }

        public bool IsEmpty()
        {
            return CurrentItem == null || CurrentItemCount == 0;
        }

        public void UseItem()
        {
            if (CurrentItemCount > 0 && CurrentItem != null && CanUseItem && !Input.GetButton(Constants.Input.ActivateAbilityKeyboard))
            {
                InstantiateItem();
                SetCount(CurrentItemCount - 1);
                OnItemUse.Invoke(CurrentItem);
                StartCoroutine(AntiSpamRoutine());
            }
        }

        public void SetItem(Item item, int count)
        {
            CurrentItem = item;
            CurrentItemCount = count;
            OnItemGet.Invoke(item);
            OnItemCountChange.Invoke(count);
        }

        public void SetCount(int count)
        {
            CurrentItemCount = count;
            OnItemCountChange.Invoke(CurrentItemCount);

            if (CurrentItemCount <= 0)
            {
                SetItem(null, 0);
            }
        }

        // PRIVATE

        private void InstantiateItem()
        {
            var instantiatedItem = BoltNetwork.Instantiate(CurrentItem.ItemPrefab, transform.position + transform.forward, transform.rotation);
            int usableMode;
            if (_throwingDirection.LastDirectionUp == Direction.Backward)
                usableMode = 2;
            else
                usableMode = 1;
            if (IsWallDetected)
                usableMode = 10;

            ItemThrown itemThrownEvent = ItemThrown.Create();
            itemThrownEvent.OwnerNickname = GetComponentInParent<PlayerInfo>().Nickname;
            itemThrownEvent.OwnerID = state.OwnerID;
            itemThrownEvent.Team = state.Team;
            itemThrownEvent.Entity = instantiatedItem;
            itemThrownEvent.UsageMode = usableMode;

            var itemOwnership = instantiatedItem.GetComponent<Ownership>();
            if (itemOwnership)
            {
                itemOwnership.Label = CurrentItem.Name;
                itemOwnership.Set(GetComponentInParent<PlayerInfo>());
                itemThrownEvent.ItemName = itemOwnership.Label;
            }

            var throwable = instantiatedItem.GetComponent<Throwable>();
            if (throwable)
            {
                _projectileLauncher.Throw(throwable, _throwingDirection.LastDirectionUp);
            }

            /*
            var usable = instantiatedItem.GetComponent<MultiModeUsable>();
            if (usable)
            {
                if (_throwingDirection.LastDirectionUp == Direction.Backward)
                {
                    usable.SetMode(2);
                }
                else
                {
                    usable.SetMode(1);
                }
            }
            */

            itemThrownEvent.Send();
        }

        private IEnumerator AntiSpamRoutine()
        {
            CanUseItem = false;
            yield return new WaitForSeconds(_antiSpamDuration);
            CanUseItem = true;
        }
    }
}
