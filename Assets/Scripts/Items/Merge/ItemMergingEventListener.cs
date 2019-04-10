using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items.Merge
{
    [DisallowMultipleComponent]
    public class ItemMergingEventListener : GlobalEventListener
    {
        [Header("Unity Events")]
        public UnityEvent OnItemMergingShield;
        public UnityEvent OnItemMergingSpeedBoost;

        public override void OnEvent(ItemMerging evnt)
        {
            if (evnt.Entity == GetComponentInParent<BoltEntity>()) // This is the kart that launched the event
            {
                if (evnt.Shield && OnItemMergingShield != null)
                {
                    OnItemMergingShield.Invoke();
                }
                else if (!evnt.Shield && OnItemMergingSpeedBoost != null)
                {
                    OnItemMergingSpeedBoost.Invoke();
                }
            }
        }
    }
}
