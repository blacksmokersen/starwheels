using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer;

namespace Items
{
    public class ItemLagCompensation : EntityBehaviour
    {
        protected void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsClient && entity.isAttached && entity.isOwner)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartTrigger) && !other.GetComponentInParent<BoltEntity>().isOwner)
                {
                    Debug.LogError("LOCAL HIT : SENT TO SERVER TO CHECK");

                    ItemsLagCheckEvent itemsLagCheckEvent = ItemsLagCheckEvent.Create();
                    itemsLagCheckEvent.ItemBoltEntity = GetComponentInParent<BoltEntity>();
                    itemsLagCheckEvent.TargetBoltEntity = other.GetComponentInParent<BoltEntity>();
                    itemsLagCheckEvent.FrameToRewindTo = BoltNetwork.Frame;
                    itemsLagCheckEvent.ItemCollisionPosition = transform.position;
                    itemsLagCheckEvent.Send();
                }
            }
        }
    }
}
