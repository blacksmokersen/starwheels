using UnityEngine;
using Bolt;

namespace Items
{
    public class KartCollisionTrigger : EntityBehaviour<IKartState>
    {
        [SerializeField] private ItemCollision _itemCollision;

        [Header("Invincibility Condition")]
        [SerializeField] private Health.Health _health;

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer && entity.isAttached)
            {
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox)) // It is an item collision
                {
                    var itemCollisionTrigger = other.GetComponent<ItemCollisionTrigger>();

                    if (itemCollisionTrigger.ItemCollision.HitsPlayer) // It is an item that damages the player
                    {
                        BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                        IItemState itemState;

                        if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item & it is attached
                        {
                            /* Hit Ourself
                            if (itemState.OwnerID == state.OwnerID)
                            {
                                if (itemCollisionTrigger.ItemCollision.ItemName == ItemCollisionName.Disk)
                                {
                                    if (other.GetComponentInParent<DiskBehaviour>().CanHitOwner)
                                    {
                                        if (!_health.IsInvincible) // The server checks that this kart is not invincible
                                        {
                                            SendPlayerHitEvent(itemState);
                                        }
                                        DestroyColliderObject(other);
                                    }
                                }
                            }
                            */
                            if (itemState.Team != state.Team)
                            {
                                if (!_health.IsInvincible) // The server checks that this kart is not invincible
                                {
                                    SendPlayerHitEvent(itemState);
                                }
                                DestroyColliderObject(other);
                            }
                        }
                    }
                }
            }
        }

        private void DestroyColliderObject(Collider other)
        {
            var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
            if (otherItemCollision.ShouldBeDestroyed(_itemCollision)
                && other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.IonBeamLaser) // The item should be destroyed
            {
                DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                destroyEntityEvent.Send();
            }
        }

        private void SendPlayerHitEvent(IItemState itemState)
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.KillerID = itemState.OwnerID;
            playerHitEvent.KillerName = itemState.OwnerNickname;
            playerHitEvent.KillerTeam = itemState.Team;
            playerHitEvent.Item = itemState.Name;
            playerHitEvent.VictimEntity = entity;
            playerHitEvent.VictimID = state.OwnerID;
            playerHitEvent.VictimName = state.Nickname;
            playerHitEvent.VictimTeam = state.Team;
            playerHitEvent.Send();
        }
    }
}
