using UnityEngine;

namespace Items
{
    public class KartCollisionTrigger : ItemCollisionTrigger
    {
        private IKartState _kartState;

        private void Awake()
        {
            _kartState = entity.GetState<IKartState>();
        }

        private new void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox))
                {
                    IItemState itemState = other.GetComponent<BoltEntity>().GetState<IItemState>();

                    if (itemState != null) // It is a concrete item
                    {
                        if (itemState.Team != _kartState.Team || itemState.OwnerID == 1)
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