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
            if (Input.GetButtonDown(Constants.Input.UseItem))
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
                OnItemUse.Invoke(CurrentItem);
                SetCount(CurrentItemCount - 1);
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
            OnItemGet.Invoke(item);
            CurrentItemCount = count;
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
                var throwable = instantiatedItem.GetComponent<Throwable>();
                _projectileLauncher.Throw(throwable);
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
