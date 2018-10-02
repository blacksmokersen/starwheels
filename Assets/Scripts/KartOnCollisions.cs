using UnityEngine;
using Items;

namespace Kart
{
    public class KartOnCollisions : MonoBehaviour {

        //private KartInventory _kartInventory;

        // CORE

        private void Awake()
        {
            //_kartInventory = GetComponentInChildren<KartInventory>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsGround(collision.gameObject))
            {
                //_kartEvents.OnCollisionEnterGround();
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == Constants.Tag.ItemBox)// && _kartInventory.IsEmpty())
            {
                StartCoroutine(collision.gameObject.GetComponent<ItemBox>().StartCooldown());
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
            return gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground);
        }
    }
}
