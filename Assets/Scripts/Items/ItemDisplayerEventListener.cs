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

            Debug.Log("Received display event : " + evnt.ItemCount);

            if (entity == evnt.Entity)
            {
                var direction = evnt.DisplayForward ? Direction.Forward : Direction.Backward;
                _itemDisplayer.DisplayItem(evnt.ItemName, evnt.ItemCount, direction);
            }
        }
    }
}
