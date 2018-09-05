using UnityEngine;
using Items;

namespace Kart {
    /*
     * Class for handling every trigger and collisions related to the kart
     */
    public class KartOnCollisions : MonoBehaviour {

        private KartEvents _kartEvents;
        private KartInventory _kartInventory;

        // CORE

        private void Awake()
        {
            _kartEvents = GetComponent<KartEvents>();
            _kartInventory = GetComponentInChildren<KartInventory>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsGround(collision.gameObject))
            {
                _kartEvents.OnCollisionEnterGround();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == Constants.ItemBoxTag && _kartInventory.IsEmpty())
            {
                StartCoroutine(collision.gameObject.GetComponent<ItemBox>().Activate());
                _kartEvents.OnGetItemBox();
            }
            else if (IsGround(collision.gameObject))
            {
                //GetComponent<KartEngine>().rb.constraints = RigidbodyConstraints.FreezeRotationY;
            }
        }

        private void OnTriggerExit(Collider trigger)
        {
            if (IsGround(trigger.gameObject))
            {
                //GetComponent<KartEngine>().rb.constraints = RigidbodyConstraints.None;
            }
        }

        // PRIVATE

        private bool IsGround(GameObject gameObject)
        {
            return gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer);
        }
    }
}
