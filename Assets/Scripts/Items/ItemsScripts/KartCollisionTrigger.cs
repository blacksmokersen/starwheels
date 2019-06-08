using UnityEngine;
using Bolt;
using System.Collections.Generic;

namespace Items
{
    [DisallowMultipleComponent]
    public class KartCollisionTrigger : EntityBehaviour<IKartState>
    {
        [SerializeField] private ItemCollision _itemCollision;

        [Header("Invincibility Condition")]
        [SerializeField] private Health.Health _health;

        [Header("Settings")]
        [SerializeField] private float _secondsSecurityBetweenHits;

        private Dictionary<int, float> _hitSecurity = new Dictionary<int, float>();
        private List<int> idsToRemove = new List<int>();

        // CORE

        private void Update()
        {
            foreach (var key in _hitSecurity.Keys)
            {
                _hitSecurity[key] += Time.deltaTime;

                if (_hitSecurity[key] > _secondsSecurityBetweenHits)
                {
                    idsToRemove.Add(key);
                }
            }
            foreach(var id in idsToRemove)
            {
                _hitSecurity.Remove(id);
            }
            idsToRemove.Clear();
        }

        // PUBLIC

        public void CheckTargetInformationsBeforeSendingHitEvent(Collider target)
        {
            var itemCollisionTrigger = target.GetComponent<ItemCollisionTrigger>();

            if (itemCollisionTrigger.ItemCollision.HitsPlayer) // It is an item that damages the player
            {
                BoltEntity itemEntity = target.GetComponentInParent<BoltEntity>();
                Ownership itemOwnership = itemEntity.GetComponent<Ownership>();

                if (itemEntity.isAttached) // It is a concrete item & it is attached
                {
                    if ((int)itemOwnership.Team != state.Team && !_hitSecurity.ContainsKey(itemOwnership.OwnerID))
                    {
                        if (!_health.IsInvincible) // The server checks that this kart is not invincible
                        {
                            Debug.Log("[HIT] Added to security.");
                            _hitSecurity.Add(itemOwnership.OwnerID, 0f);
                            SendPlayerHitEvent(itemOwnership);
                        }
                        DestroyColliderObject(target);
                    }
                    else if (_hitSecurity.ContainsKey(itemOwnership.OwnerID))
                    {
                        Debug.Log("[HIT] Security hit.");
                    }
                }
            }
        }

        /*
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
                        Ownership itemOwnership = itemEntity.GetComponent<Ownership>();

                        if (itemEntity.isAttached) // It is a concrete item & it is attached
                        {
                          //   Hit Ourself
                          //  if (itemState.OwnerID == state.OwnerID)
                          //  {
                          //      if (itemCollisionTrigger.ItemCollision.ItemName == ItemCollisionName.Disk)
                          //      {
                          //          if (other.GetComponentInParent<DiskBehaviour>().CanHitOwner)
                          //          {
                          //              if (!_health.IsInvincible) // The server checks that this kart is not invincible
                          //              {
                          //                  SendPlayerHitEvent(itemState);
                          //              }
                          //              DestroyColliderObject(other);
                          //          }
                          //      }
                          //  }

                            if ((int)itemOwnership.Team != state.Team)
                            {
                                if (!_health.IsInvincible) // The server checks that this kart is not invincible
                                {
                                    SendPlayerHitEvent(itemOwnership);
                                }
                                DestroyColliderObject(other);
                            }
                        }
                    }
                }
            }
        } */

        private void DestroyColliderObject(Collider other)
        {
            var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;
            if (otherItemCollision.ShouldBeDestroyed(_itemCollision)
                && otherItemCollision.ItemName != ItemCollisionName.IonBeamLaser) // The item should be destroyed
            {
                DestroyEntity destroyEntityEvent = DestroyEntity.Create();
                destroyEntityEvent.Entity = other.GetComponentInParent<BoltEntity>();
                destroyEntityEvent.Send();
            }
        }

        private void SendPlayerHitEvent(Ownership itemOwnership)
        {
            PlayerHit playerHitEvent = PlayerHit.Create();
            playerHitEvent.KillerID = itemOwnership.OwnerID;
            playerHitEvent.KillerName = itemOwnership.OwnerNickname;
            playerHitEvent.KillerTeam = (int) itemOwnership.Team;
            playerHitEvent.Item = itemOwnership.Label;
            playerHitEvent.VictimEntity = entity;
            playerHitEvent.VictimID = state.OwnerID;
            playerHitEvent.VictimName = GetComponentInParent<Multiplayer.PlayerInfo>().Nickname;
            playerHitEvent.VictimTeam = state.Team;
            playerHitEvent.Send();
        }
    }
}
