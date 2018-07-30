using UnityEngine;
using Items;

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
                // Kart.SetState(Grounded)
                FindObjectOfType<KartStates>().AirState = AirStates.Grounded;
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == Constants.ItemBoxTag)
            {
                StartCoroutine(collision.gameObject.GetComponent<ItemBox>().Activate());
                StartCoroutine(GetComponentInChildren<KartInventory>().GetLotteryItem());
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            { 
                FindObjectOfType<KartEngine>().rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }
        }
        private void OnTriggerExit(Collider trigger)
        {
            if (trigger.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                FindObjectOfType<KartEngine>().rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
