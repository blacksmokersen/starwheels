using UnityEngine;

namespace Common.PhysicsUtils
{
    public class GravitySimulator : MonoBehaviour
    {
        public float GravityForce;
        public bool UseWorldCoordinates;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
        }

        public void ApplyGravity()
        {
            if (UseWorldCoordinates)
            {
                _rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            }
            else
            {
                _rb.AddRelativeForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            }
        }
    }
}
