using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ThrowableLauncher))]
    public class Inventory : MonoBehaviour, IControllable
    {
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
        }

        private void Update()
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
            if (CurrentItemCount > 0 || CurrentItem != null)
            {
                var instantiatedItem = BoltNetwork.Instantiate(CurrentItem.itemPrefab);
                if (CurrentItem.ItemType == ItemType.Throwable)
                {
                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _projectileLauncher.Throw(throwable);
                }
                OnItemUse.Invoke(CurrentItem);
                SetCount(CurrentItemCount - 1);
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

    }
}
