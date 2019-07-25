using UnityEngine;
using UnityEngine.Events;
using Bolt;
using Multiplayer;

namespace Items
{
    public class ItemLagCompensation : EntityBehaviour
    {
        [SerializeField] private float _collisionDistanceCheck;
        private BoltHitboxBody boltHitboxBody;

        //CORE

        protected void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsClient && entity.isAttached && entity.isOwner)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartTrigger) && !other.GetComponentInParent<BoltEntity>().isOwner)
                {
                    //  Debug.LogError("LOCAL HIT : SENT TO SERVER TO CHECK");
                    boltHitboxBody = GetComponentInParent<BoltHitboxBody>();

                    foreach (BoltHitbox hitbox in boltHitboxBody.hitboxes)
                    {
                        ItemCollsionLagCompensationEvent itemsLagCheckEvent = ItemCollsionLagCompensationEvent.Create();
                        itemsLagCheckEvent.ItemBoltEntity = GetComponentInParent<BoltEntity>();
                        itemsLagCheckEvent.TargetBoltEntity = other.GetComponentInParent<BoltEntity>();
                        itemsLagCheckEvent.FramesToRewind = 15;
                        itemsLagCheckEvent.CollisionDistanceCheck = _collisionDistanceCheck;
                        itemsLagCheckEvent.ItemCollsionPosition = hitbox.transform.position;
                        itemsLagCheckEvent.Send();
                    }
                }
            }
            else if (BoltNetwork.IsServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.KartTrigger) && !other.GetComponentInParent<BoltEntity>().isOwner)
                {
                    //    Debug.LogError("LOCAL HIT BY SERVER");
                    boltHitboxBody = GetComponentInParent<BoltHitboxBody>();

                    foreach (BoltHitbox hitbox in boltHitboxBody.hitboxes)
                    {
                        ItemCollsionLagCompensationEvent itemsLagCheckEvent = ItemCollsionLagCompensationEvent.Create();
                        itemsLagCheckEvent.ItemBoltEntity = GetComponentInParent<BoltEntity>();
                        itemsLagCheckEvent.TargetBoltEntity = other.GetComponentInParent<BoltEntity>();
                        itemsLagCheckEvent.FramesToRewind = 15;
                        itemsLagCheckEvent.CollisionDistanceCheck = _collisionDistanceCheck;
                        itemsLagCheckEvent.ItemCollsionPosition = hitbox.transform.position;
                        itemsLagCheckEvent.Send();
                    }
                }
            }
        }

        //PUBLIC

        public void SetCollisionDistanceCheck(float value)
        {
            _collisionDistanceCheck = value;
        }

        public float GetCollisionDistanceCheck()
        {
            return _collisionDistanceCheck;
        }
    }
}
