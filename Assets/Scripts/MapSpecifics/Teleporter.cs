using UnityEngine;

namespace MapsSpecifics
{
    [RequireComponent(typeof(Collider))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private GameObject _out;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartHealthHitBox))
            {
                var kartRoot = other.GetComponentInParent<BoltEntity>().gameObject;
                kartRoot.transform.position = _out.transform.position;
                kartRoot.transform.rotation = _out.transform.rotation;
                var kartRb = kartRoot.GetComponent<Rigidbody>();
                var newVelocity = kartRb.velocity.magnitude * _out.transform.forward.normalized;
                Debug.Log("Old velocity : " + kartRb.velocity);
                Debug.Log("New velocity : " + newVelocity);
                Debug.Log("Orientation : " +  _out.transform.rotation.eulerAngles.normalized);
                kartRb.velocity = newVelocity;
            }
        }
    }
}
