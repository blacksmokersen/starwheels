using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items
{
    public class ItemCollisionTrigger : EntityBehaviour
    {

        [SerializeField] ItemActivationBehaviour itemActivationBehaviour;

        [Header("Events")]
        public UnityEvent OnCollision;

        [Header("Me")]
        public ItemCollision ItemCollision;

        protected void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer)
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

        private void OnTriggerExit(Collider other)
        {
         if(itemActivationBehaviour != null)
            itemActivationBehaviour.Activated = true;
        }

        private void DestroySelf()
        {
            DestroyEntity destroyEntityEvent = DestroyEntity.Create();
            destroyEntityEvent.Entity = entity;
            destroyEntityEvent.Send();
        }
    }
}
