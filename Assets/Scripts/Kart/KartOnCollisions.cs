using UnityEngine;
using Items;

namespace Kart {
    /*
     * Class for handling every trigger and collisions related to the kart
     * 
     */ 
    public class KartOnCollisions : BaseKartComponent {

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                kartEvents.OnCollisionEnterGround();
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == Constants.ItemBoxTag)
            {
                StartCoroutine(collision.gameObject.GetComponent<ItemBox>().Activate());
                if(kartEvents.OnCollisionEnterItemBox != null)
                    kartEvents.OnCollisionEnterItemBox();
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                GetComponent<KartEngine>().rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }
        }
        private void OnTriggerExit(Collider trigger)
        {
            if (trigger.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                GetComponent<KartEngine>().rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
