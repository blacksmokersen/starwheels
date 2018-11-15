using UnityEngine;
using UnityEngine.Events;
using Bolt;

namespace Items
{
    public class ItemCollisionTrigger : MonoBehaviour
    {
        [Header("Events")]
        public UnityEvent OnCollision;

        [Header("Me")]
        public ItemCollision ItemCollision;

        private void OnTriggerEnter(Collider other)
        {
            if (BoltNetwork.isServer)
            {
                if (other.gameObject.CompareTag(Constants.Tag.CollisionHitBox))
                {
                    OnCollision.Invoke();
                    ItemCollision.CheckCollision(other.GetComponent<ItemCollision>());
                }
            }
        }

        private void DestroySelf()
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}
