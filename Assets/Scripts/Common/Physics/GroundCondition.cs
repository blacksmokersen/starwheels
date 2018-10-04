using UnityEngine;

namespace Common.PhysicsUtils
{
    public class GroundCondition : MonoBehaviour
    {
        [Header("State")]
        public bool Grounded;

        [Header("Parameters")]
        [SerializeField] private float distanceForGrounded;
        [SerializeField] private Vector3 offset;

        private void FixedUpdate()
        {
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position + offset,
                transform.TransformDirection(Vector3.down),
                distanceForGrounded,
                1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                Grounded = true;
            }
            else
            {
                Grounded = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 direction = transform.TransformDirection(Vector3.down) * distanceForGrounded;
            Gizmos.DrawRay(transform.position + offset, direction);
        }
    }
}
