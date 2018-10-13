using UnityEngine;
using UnityEngine.Events;

namespace Common.PhysicsUtils
{
    public class GroundCondition : MonoBehaviour
    {
        [Header("State")]
        public bool Grounded = false;

        [Header("Parameters")]
        [SerializeField] private float distanceForGrounded;
        [SerializeField] private Vector3 offset;

        [Header("Events")]
        public UnityEvent OnHitGround;
        public UnityEvent OnLeftGround;

        private void FixedUpdate()
        {
            CheckGrounded();
        }

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position + offset,
                transform.TransformDirection(Vector3.down),
                distanceForGrounded,
                1 << LayerMask.NameToLayer(Constants.Layer.Ground))
                && !Grounded)
            {
                Grounded = true;
                OnHitGround.Invoke();
            }
            else if(Grounded)
            {
                Grounded = false;
                OnLeftGround.Invoke();
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
