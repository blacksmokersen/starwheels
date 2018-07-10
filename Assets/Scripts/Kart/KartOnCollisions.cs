using UnityEngine;

namespace Kart {
    public class KartOnCollisions : MonoBehaviour {

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                FindObjectOfType<KartStates>().AirState = AirStates.Grounded;
            }
        }
        private void OnTriggerEnter(Collider trigger)
        {
            FindObjectOfType<KartPhysics>().rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        private void OnTriggerExit(Collider trigger)
        {
            FindObjectOfType<KartPhysics>().rb.constraints = RigidbodyConstraints.None;
        }
    }
}
