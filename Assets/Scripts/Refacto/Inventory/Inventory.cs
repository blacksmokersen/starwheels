using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(ThrowableLauncher))]
    public class Inventory : MonoBehaviour, IControllable
    {
        public ItemEvent OnItemGet;
        public ItemEvent OnItemUse;
        public IntEvent OnItemCountChange;

        private ThrowableLauncher _projectileLauncher;
        private Item _currentItem;
        private int _currentItemCount;

        // CORE
        public void MapInputs()
        {
            if (Input.GetButtonDown("Item"))
            {
                UseItem();
            }
        }

        // PUBLIC
        public void UseItem()
        {
            if (_currentItemCount > 0 || _currentItem != null)
            {
                var instantiatedItem = BoltNetwork.Instantiate(_currentItem.itemPrefab);
                if (_currentItem.ItemType == ItemType.Throwable)
                {
                    var throwable = instantiatedItem.GetComponent<Throwable>();
                    _projectileLauncher.Throw(throwable);
                }
                OnItemUse.Invoke(_currentItem);
                SetCount(_currentItemCount - 1);
            }
        }

        public void SetItem(Item item)
        {
            _currentItem = item;
            OnItemGet.Invoke(item);
        }

        public void SetCount(int count)
        {
            _currentItemCount = count;
            OnItemCountChange.Invoke(_currentItemCount);

            if (_currentItemCount == 0)
                SetItem(null);
        }

        // PRIVATE
    }
}
