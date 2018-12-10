using UnityEngine;
using Bolt;

namespace Items
{
    [RequireComponent(typeof(ItemDisplayer))]
    public class ItemDisplayerEventListener : GlobalEventListener
    {
        private ItemDisplayer _itemDisplayer;

        // CORE

        private void Awake()
        {
            _itemDisplayer = GetComponent<ItemDisplayer>();
        }

        // BOLT

        public override void OnEvent(ShowKartDisplayItem evnt)
        {
            var entity = GetComponentInParent<BoltEntity>();
            Debug.Log("Received event for : " + evnt.ItemName);

            if (entity == evnt.Entity)
            {
                Direction direction = (Direction) System.Enum.Parse(typeof(Direction), evnt.Direction);
                _itemDisplayer.DisplayItem(evnt.ItemName, evnt.ItemCount, direction);
            }
        }
    }
}
