using UnityEngine;
using Bolt;

namespace Items
{
    public class KartCollisionTrigger : EntityBehaviour<IKartState>
    {
        [SerializeField] private ItemCollision _itemCollision;

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer && entity.isAttached)
            {
                if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox))
                {
                    BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                    IItemState itemState;                    

                    if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                    {
                        if (itemState.Team != state.Team || itemState.OwnerID == state.OwnerID) // It's a hit
                        {
                            PlayerHit playerHitEvent = PlayerHit.Create();
                            playerHitEvent.PlayerEntity = entity;
                            playerHitEvent.Send();
                        }

                        var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
                        if (otherItemCollision.ShouldBeDestroyed(_itemCollision)) // The item should be destroyed
                        {
                            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                            destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                            destroyEntityEvent.Send();
                        }
                    }
                }
            }
        }
    }
}
