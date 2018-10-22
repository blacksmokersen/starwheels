﻿using UnityEngine;

namespace Items.Debug
{
    [RequireComponent(typeof(Inventory))]
    public class ItemSwitcher : MonoBehaviour, IControllable
    {
        private Item[] _itemsList;
        private Inventory _inventory;
        private int _actualItemIndex = 0;

        // CORE

        private void Awake()
        {
            _itemsList = Resources.Load<ItemListData>("Data/ItemList").Items;
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            MapInputs();
        }

        // PUBLIC

        public void MapInputs()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SwitchToNextItem();
            }
        }

        // PRIVATE

        private void SwitchToNextItem()
        {
            var nextItem = _itemsList[(_actualItemIndex++) % _itemsList.Length];
            _inventory.SetItem(nextItem, 50);
        }
    }
}
