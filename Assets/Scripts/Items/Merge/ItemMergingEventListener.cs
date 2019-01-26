using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items.Merge
{
    public class ItemMergingEventListener : GlobalEventListener
    {
        [Header("Unity Events")]
        public UnityEvent OnFullItemMerging;
        public UnityEvent OnSmallItemMerging;

        public override void OnEvent(ItemMerging evnt)
        {
            if (evnt.Entity == GetComponentInParent<BoltEntity>()) // This is the kart that launched the event
            {
                if (evnt.Full)
                {
                    if (OnFullItemMerging != null)
                    {
                        OnFullItemMerging.Invoke();
                    }
                }
                else
                {
                    if (OnSmallItemMerging != null)
                    {
                        OnSmallItemMerging.Invoke();
                    }
                }
            }
        }
    }
}
