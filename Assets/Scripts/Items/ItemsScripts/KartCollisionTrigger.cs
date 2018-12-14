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
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox) &&
                    other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.Totem) // It is an item collision (except totem)
                {
                    BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                    IItemState itemState;

                    if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                    {
                        if (itemState.OwnerID == state.OwnerID || (itemState.OwnerID == 0 && itemState.Team == new Color(0,0,0,0)))
                        {
                            if (other.GetComponentInChildren<ItemActivationBehaviour>().Activated)
                            {
                                SendPlayerHitEvent(itemState);
                            }
                        }

                        else if (itemState.Team != state.Team)
                        {
                            SendPlayerHitEvent(itemState);
                        }

                        var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
                        if (otherItemCollision.ShouldBeDestroyed(_itemCollision) && other.GetComponentInChildren<ItemActivationBehaviour>().Activated) // The item should be destroyed
                        {
                            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                            destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                            destroyEntityEvent.Send();
                        }
                    }
                }
            }
        }

        private void SendPlayerHitEvent(IItemState itemState)
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.PlayerEntity = entity;
            playerHitEvent.KillerName = itemState.OwnerNickname;
            playerHitEvent.KillerTeamColor = itemState.Team;
            playerHitEvent.Item = itemState.Name;
            playerHitEvent.VictimName = state.Nickname;
            playerHitEvent.VictimTeamColor = state.Team;
            playerHitEvent.Send();
        }
    }
}
