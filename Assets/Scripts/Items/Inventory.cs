using UnityEngine;
using Multiplayer;
using Bolt;
using ThrowingSystem;
using System.Collections;

namespace Items
{
    [RequireComponent(typeof(ThrowableLauncher))]
    public class Inventory : EntityBehaviour<IKartState>, IControllable
    {
        public bool CanUseItem = true;

        [Header("Spam Prevention")]
        [SerializeField] private float _antiSpamDuration;

        [Header("Current Item")]
        public Item CurrentItem;
        public int CurrentItemCount;

        [Header("Events")]
        public ItemEvent OnItemGet;
        public ItemEvent OnItemUse;
        public IntEvent OnItemCountChange;

        private ThrowableLauncher _projectileLauncher;

        // CORE

        private void Awake()
        {
            _projectileLauncher = GetComponent<ThrowableLauncher>();
            CurrentItem = null;
            CurrentItemCount = 0;
        }

        // BOLT

        public override void SimulateController()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetButtonDown(Constants.Input.UseItem) ||
                Input.GetButtonDown(Constants.Input.UseItemBackward) ||
                Input.GetButtonDown(Constants.Input.UseItemForward))
            {
                UseItem();
            }
        }

        public bool IsEmpty()
        {
            return CurrentItem == null || CurrentItemCount == 0;
        }

        public void UseItem()
        {
            if (CurrentItemCount > 0 && CurrentItem != null && CanUseItem)
            {
                InstantiateItem();
                SetCount(CurrentItemCount - 1);
                OnItemUse.Invoke(CurrentItem);
                StartCoroutine(AntiSpamRoutine());
            }
        }

        public void SetItem(Item item)
        {
            CurrentItem = item;
            OnItemGet.Invoke(item);
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

            if (CurrentItemCount == 0)
                SetItem(null);
        }

        // PRIVATE

        private void InstantiateItem()
        {
            var instantiatedItem = BoltNetwork.Instantiate(CurrentItem.itemPrefab);

            var itemOwnership = instantiatedItem.GetComponent<Ownership>();
            var playerSettings = GetComponentInParent<Player>();
            itemOwnership.Set(playerSettings);

            if (CurrentItem.ItemType == ItemType.Throwable)
            {
                var itemState = instantiatedItem.GetComponent<BoltEntity>().GetState<IItemState>();
                itemState.Team = state.Team;
                itemState.OwnerID = state.OwnerID;
                var throwable = instantiatedItem.GetComponent<Throwable>();
                _projectileLauncher.Throw(throwable, _projectileLauncher.GetThrowingDirection());
            }
        }

        private IEnumerator AntiSpamRoutine()
        {
            CanUseItem = false;
            yield return new WaitForSeconds(_antiSpamDuration);
            CanUseItem = true;
        }
    }
}
