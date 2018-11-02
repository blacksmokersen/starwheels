using UnityEngine;
using Bolt;

namespace Items
{
    /*
     * Collider for detecting collision of the rocket with another kart
     * Calls the Collision method of RocketBehaviour for particle effect and Health Loss
     */
    [RequireComponent(typeof(Collider))]
    public class RocketPlayerTrigger : EntityBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.Tag.HealthHitBox) && entity.isOwner)
            {
                GetComponentInParent<RocketBehaviour>().CheckCollision(other.gameObject);
            }
            else if (other.gameObject.CompareTag(Constants.Tag.DiskItem))
            {
                other.gameObject.GetComponentInParent<NetworkDestroyable>().DestroyObject();
                GetComponentInParent<NetworkDestroyable>().DestroyObject();
            }
            else if (other.gameObject.CompareTag(Constants.Tag.RocketItem))
            {
                GetComponentInParent<NetworkDestroyable>().DestroyObject();
            }
        }
    }
}
