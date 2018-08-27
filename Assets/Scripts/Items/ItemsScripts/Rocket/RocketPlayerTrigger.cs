using UnityEngine;

namespace Items {
    /* 
     * Collider for detecting collision of the rocket with another kart
     * Calls the Collision method of RocketBehaviour for particle effect and Health Loss
     * 
     */ 
    [RequireComponent(typeof(Collider))]
    public class RocketPlayerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartRigidBodyTag)
            {
                GetComponentInParent<RocketBehaviour>().CheckCollision(other);
            }
        }
    }
}
