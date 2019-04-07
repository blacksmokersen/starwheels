using UnityEngine;

namespace MapsSpecifics
{
    [RequireComponent(typeof(Collider))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private GameObject _out;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartHealthHitBox) || other.CompareTag(Constants.Tag.ItemCollisionHitBox))
            {
                var objectRoot = other.GetComponentInParent<BoltEntity>().gameObject;
                objectRoot.transform.position = _out.transform.position;
                objectRoot.transform.rotation = _out.transform.rotation;
                var rb = objectRoot.GetComponent<Rigidbody>();
                var newVelocity = rb.velocity.magnitude * _out.transform.forward.normalized;
                rb.velocity = newVelocity;
            }
        }
    }
}
