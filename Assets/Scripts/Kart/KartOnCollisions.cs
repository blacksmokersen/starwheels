using UnityEngine;

namespace Kart {
    /*
     * Class for handling every trigger and collisions related to the kart
     * 
     */ 
    public class KartOnCollisions : MonoBehaviour {

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                FindObjectOfType<KartStates>().AirState = AirStates.Grounded;
            }
        }
    }
}
