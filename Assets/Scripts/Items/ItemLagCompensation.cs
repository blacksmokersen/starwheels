using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer;

namespace Items
{
    public class ItemLagCompensation : EntityBehaviour
    {
        [SerializeField] private float _collisionDistanceCheck;

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
                    itemsLagCheckEvent.FramesToRewind = 15;
                    itemsLagCheckEvent.CollsionDistanceCheck = _collisionDistanceCheck;
                    itemsLagCheckEvent.ItemCollisionPosition = transform.position;
                    itemsLagCheckEvent.Send();
                }
            }
            else if (BoltNetwork.IsServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartTrigger) && !other.GetComponentInParent<BoltEntity>().isOwner)
                {
                    Debug.LogError("LOCAL HIT BY SERVER");

                    ItemsLagCheckEvent itemsLagCheckEvent = ItemsLagCheckEvent.Create();
                    itemsLagCheckEvent.ItemBoltEntity = GetComponentInParent<BoltEntity>();
                    itemsLagCheckEvent.TargetBoltEntity = other.GetComponentInParent<BoltEntity>();
                    itemsLagCheckEvent.FramesToRewind = 0;
                    itemsLagCheckEvent.CollsionDistanceCheck = _collisionDistanceCheck;
                    itemsLagCheckEvent.ItemCollisionPosition = transform.position;
                    itemsLagCheckEvent.Send();
                }
            }
        }
    }
}
