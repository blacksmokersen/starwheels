using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public class ItemCollisionTrigger : EntityBehaviour
    {
        [Header("Events")]
        public UnityEvent OnCollision;

        [Header("Me")]
        public ItemCollision ItemCollision;

        protected void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.IsServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.ItemCollisionHitBox))
                {
                    var otherItemCollision = other.GetComponent<ItemCollisionTrigger>().ItemCollision;

                    if (ItemCollision.ShouldBeDestroyed(otherItemCollision))
                    {
                        OnCollision.Invoke();
                        DestroySelf();
                    }
                }
            }
        }

        private void DestroySelf()
        {
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = entity;
            destroyEntityEvent.Send();
        }
    }
}
