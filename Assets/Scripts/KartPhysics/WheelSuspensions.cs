using UnityEngine;

namespace KartPhysics
{
    public class WheelSuspensions : MonoBehaviour
    {
        [Header("Suspensions Settings")]
        public float MaxExtensionDistance;
        public float SuspensionStiffness;
        public float ForceToApply;

        [Header("Wheels transforms")]
        [SerializeField] private Transform[] wheelsTransforms;

        private Rigidbody _rb;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            foreach (var wheelTransform in wheelsTransforms)
            {
                AdjustWheelPosition(wheelTransform);
            }
        }

        // PUBLIC

        // PRIVATE

        private void AdjustWheelPosition(Transform wheelTransform)
        {
            RaycastHit hit;

            if (Physics.Raycast(wheelTransform.position, Vector3.down, out hit, MaxExtensionDistance, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                var distance = Mathf.Clamp(hit.distance, 0, MaxExtensionDistance);
                var compressionRatio = -distance + MaxExtensionDistance;
                _rb.AddForceAtPosition(ComputeForceToAdd(compressionRatio), wheelTransform.position, ForceMode.Acceleration);
            }
        }

        private Vector3 ComputeForceToAdd(float compressionRatio)
        {
            var newSuspensionForce = transform.TransformVector(Vector3.up * compressionRatio * SuspensionStiffness);
            return newSuspensionForce * ForceToApply;
        }
    }
}
