using UnityEngine;
using Bolt;

namespace Items
{
    public class KartCollisionTrigger : EntityBehaviour<IKartState>
    {
        [SerializeField] private ItemCollision _itemCollision;

        private bool _imunity;
        private GameObject _imunityTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer && entity.isAttached)
            {
                if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox) &&
                    other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.Totem) // It is an item collision (except totem)
                {
                    BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                    IItemState itemState;

                    if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                    {
                        if (itemState.OwnerID == state.OwnerID)
                        {

                            if (!_imunity && _imunityTarget != other.gameObject)
                            {
                                _imunityTarget = other.gameObject;
                                _imunity = true;
                                Debug.LogError("imunity true");
                            }

                            if(/*other.gameObject == _imunityTarget &&*/ !_imunity)
                            {
                                Debug.LogError("kill");
                                PlayerHit playerHitEvent = PlayerHit.Create();
                                playerHitEvent.PlayerEntity = entity;
                                playerHitEvent.Send();
                            }



                        }
                        else if (itemState.Team != state.Team) // It's a hit
                        {
                            PlayerHit playerHitEvent = PlayerHit.Create();
                            playerHitEvent.PlayerEntity = entity;
                            playerHitEvent.Send();
                        }

                        var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
                        if (otherItemCollision.ShouldBeDestroyed(_itemCollision) && !_imunity) // The item should be destroyed
                        {
                            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                            destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                            destroyEntityEvent.Send();
                        }
                    }
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox) &&
                   other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.Totem) // It is an item collision (except totem)
            {
                BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                IItemState itemState;

                if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                {
                    if (itemState.Team != state.Team || itemState.OwnerID == state.OwnerID) // It's a hit
                    {
                        if(itemState.OwnerID == state.OwnerID)
                        {
                            if (other.gameObject == _imunityTarget)
                            {
                                Debug.LogError("imunity false");
                                _imunity = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
