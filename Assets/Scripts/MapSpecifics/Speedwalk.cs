using UnityEngine;
using Common.PhysicsUtils;

namespace MapsSpecifics
{
    [DisallowMultipleComponent]
    public class Speedwalk : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _newClampMagnitude;
        [SerializeField] private float _forceToAdd;
        [SerializeField] private GameObject _directionTarget;

        // CORE

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
            {
                other.GetComponentInParent<ClampSpeed>().SetClampMagnitude(_newClampMagnitude);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
            {
                other.GetComponentInParent<Rigidbody>().AddForce(_directionTarget.transform.forward * _forceToAdd, ForceMode.Acceleration);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constants.Tag.KartCollider))
            {
                other.GetComponentInParent<ClampSpeed>().ResetClampMagnitude();
            }
        }
    }
}
