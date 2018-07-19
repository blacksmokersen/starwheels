using UnityEngine;
using Items;
using Photon;

namespace Kart {
    /*
     * Class for handling every trigger and collisions related to the kart
     * 
     */ 
    public class KartOnCollisions : PunBehaviour {

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                FindObjectOfType<KartStates>().AirState = AirStates.Grounded;
            }
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == Constants.ItemBoxTag)
            {
                StartCoroutine(collision.gameObject.GetComponent<ItemBox>().Activate());
                if (!PhotonNetwork.connected || photonView.isMine)
                    StartCoroutine(GetComponentInChildren<KartInventory>().GetLotteryItem());
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            { 
                FindObjectOfType<KartPhysics>().rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }
        }
        private void OnTriggerExit(Collider trigger)
        {
            if (trigger.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                FindObjectOfType<KartPhysics>().rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
