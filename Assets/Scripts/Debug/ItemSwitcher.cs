using UnityEngine;
using Items;
using Bolt;

namespace SW.DebugUtils
{
    [RequireComponent(typeof(Inventory))]
    public class ItemSwitcher : EntityBehaviour, IControllable
    {
        [SerializeField] private bool _enabled = false;
        [SerializeField] private bool _securityEngaged = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        private Item[] _itemsList;
        private Inventory _inventory;
        private int _actualItemIndex = 0;

        // CORE

        private void Awake()
        {
            _itemsList = Resources.Load<ItemListData>(Constants.Resources.ItemListData).Items;
            _inventory = GetComponent<Inventory>();
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
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.RightAlt) && Input.GetKeyDown(KeyCode.Equals))
            {
                _securityEngaged = !_securityEngaged;
            }

            if (Enabled && !_securityEngaged && Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToNextItem();
            }
        }

        // PRIVATE

        private void SwitchToNextItem()
        {
            var nextItem = _itemsList[(_actualItemIndex++) % _itemsList.Length];
            if (nextItem.name == "IonBeam")
                _inventory.SetItem(nextItem, 1);
            else
                _inventory.SetItem(nextItem, 50);
        }
    }
}
