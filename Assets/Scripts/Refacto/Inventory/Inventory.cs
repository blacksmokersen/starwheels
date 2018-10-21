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
        [SerializeField] private Item _currentItem;
        [SerializeField] private int _currentItemCount;

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
                Debug.Log("Using Item");
                UseItem();
            }
        }

        public void UseItem()
        {
            if (_currentItemCount > 0 || _currentItem != null)
            {
                Debug.Log("UseItem()");
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
