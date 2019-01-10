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
                    BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                    IItemState itemState;
                    Debug.Log("Is attached ? ...");
                    if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                    {
                        Debug.Log("Attached !");
                        if (!_health.IsInvincible) // The server checks that this kart is not invincible
                        {
                            if (other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName == ItemCollisionName.IonBeamLaser)
                            {
                                SendPlayerHitEvent(itemState);
                            }
                            if (itemState.OwnerID == state.OwnerID || (itemState.OwnerID == 0 && itemState.Team == new Color(0, 0, 0, 0)))
                            {
                                if (other.GetComponentInChildren<ItemActivationBehaviour>().Activated
                                    && other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.IonBeamLaser)
                                {
                                    SendPlayerHitEvent(itemState);
                                }
                            }
                            else if (itemState.Team != state.Team)
                            {
                                SendPlayerHitEvent(itemState);
                            }
                        }

                        Debug.Log("Should be destroyed ? ...");
                        var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
                        if (otherItemCollision.ShouldBeDestroyed(_itemCollision)
                            && other.GetComponentInChildren<ItemActivationBehaviour>().Activated
                            && other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.IonBeamLaser) // The item should be destroyed
                        {
                            Debug.Log("Yup !");
                            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                            destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                            destroyEntityEvent.Send();
                        }
                    }
                }
            }
        }

        // Laissez ça, c'est pour me souvenir de tester l'instakill au corps à corps via le Stay :)

        /*
        private void OnTriggerStay(Collider other)
        {
            if (BoltNetwork.IsServer && entity.isAttached)
            {
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox) &&
                    other.GetComponent<ItemCollisionTrigger>().ItemCollision.ItemName != ItemCollisionName.Totem) // It is an item collision (except totem)
                {
                    BoltEntity itemEntity = other.GetComponentInParent<BoltEntity>();
                    IItemState itemState;
                    Debug.LogError("3");
                    if (itemEntity.isAttached && itemEntity.TryFindState<IItemState>(out itemState)) // It is a concrete item
                    {
                        Debug.LogError("4");
                        if (itemState.Team != state.Team || itemState.Team != new Color(0, 0, 0, 0))
                        {
                            Debug.LogError("5");
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
        */

        private void SendPlayerHitEvent(IItemState itemState)
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.KillerName = itemState.OwnerNickname;
            playerHitEvent.KillerTeamColor = itemState.Team;
            playerHitEvent.Item = itemState.Name;
            playerHitEvent.VictimEntity = entity;
            playerHitEvent.VictimID = state.OwnerID;
            playerHitEvent.VictimName = state.Nickname;
            playerHitEvent.VictimTeamColor = state.Team;
            playerHitEvent.Send();
        }
    }
}
