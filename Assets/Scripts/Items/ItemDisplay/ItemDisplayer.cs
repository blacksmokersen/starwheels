using UnityEngine;
using Bolt;
using System;
using ThrowingSystem;

namespace Items
{
    [RequireComponent(typeof(Inventory))]
    public class ItemDisplayer : EntityBehaviour<IKartState>, IControllable
    {
        [SerializeField] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        [Header("Throwing System")]
        [SerializeField] private ThrowingDirection _throwingDirection;

        [Header("Dependencies")]
        [SerializeField] private GameObject _shieldsRoot;

        private Inventory _inventory;
        private ItemListData _itemList;
        private bool _itemIsForward = false;
        private bool _itemIsBackward = false;

        // CORE

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
            _itemList = Resources.Load<ItemListData>(Constants.Resources.ItemListData);
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
                CheckAxis();
            }
        }

        public void SendShowDisplayEvent()
        {
            var showDisplayEvent = ShowKartDisplayItem.Create(GlobalTargets.Everyone);
            showDisplayEvent.Entity = GetComponentInParent<BoltEntity>();
            showDisplayEvent.ItemName = _inventory.CurrentItem != null ? _inventory.CurrentItem.Name : "";
            showDisplayEvent.ItemCount = _inventory.CurrentItemCount;
            showDisplayEvent.Direction = (int)_throwingDirection.LastDirectionUp;
            showDisplayEvent.Send();
        }

        public void DisplayItem(string itemNameToDisplay, int itemCountToDisplay, Direction direction)
        {
            var item = _itemList.GetItemUsingName(itemNameToDisplay);
            if (item && itemCountToDisplay > 0)
            {
                foreach (var node in _shieldsRoot.GetComponentsInChildren<ItemDisplayerShieldNode>())
                {
                    if (node.Rarity == item.Rarity)
                    {
                        node.Display(direction);
                    }
                    else
                    {
                        node.Hide();
                    }
                }
            }
            else
            {
                foreach (var node in _shieldsRoot.GetComponentsInChildren<ItemDisplayerShieldNode>())
                {
                    node.Hide();
                }
            }
        }

        // PRIVATE

        private void CheckAxis()
        {
            if (Input.GetAxis(Constants.Input.UpAndDownAxis) > 0.3f && _itemIsForward == false)
            {
                _itemIsForward = true;
                _itemIsBackward = false;
                SendShowDisplayEvent();

            }
            else if (Input.GetAxis(Constants.Input.UpAndDownAxis) < -0.3f && _itemIsBackward == false)
            {
                _itemIsBackward = true;
                _itemIsForward = false;
                SendShowDisplayEvent();
            }
            else if (Math.Abs(Input.GetAxis(Constants.Input.UpAndDownAxis)) < 0.3f && (_itemIsBackward || _itemIsForward))
            {
                _itemIsForward = false;
                _itemIsBackward = false;
                SendShowDisplayEvent();
            }
        }
    }
}
