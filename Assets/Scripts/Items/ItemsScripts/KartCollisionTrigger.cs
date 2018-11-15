using UnityEngine;
using Bolt;

namespace Items
{
    public class KartCollisionTrigger : ItemCollisionTrigger
    {
        private IKartState _kartState;

        public override void Attached()
        {
            _kartState = entity.GetState<IKartState>();
        }

        private new void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox))
                {
                    IItemState itemState = other.GetComponentInParent<BoltEntity>().GetState<IItemState>();
                    Debug.Log("Item colliding with kart.");

                    if (itemState != null) // It is a concrete item
                    {
                        if (itemState.Team != _kartState.Team || itemState.OwnerID == _kartState.OwnerID)
                        {
                            PlayerHit playerHitEvent = PlayerHit.Create();
                            playerHitEvent.PlayerEntity = entity;
                            playerHitEvent.Send();
                        }
                    }                    
                }

                base.OnTriggerEnter(other);
            }
        }
    }
}