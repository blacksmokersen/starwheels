using UnityEngine;

namespace Items
{
    public class RocketBehaviour : ProjectileBehaviour
    {
        [Header("Rocket parameters")]
        public float MaxAngle;
        public float TurnSpeed;

        private RocketLockTarget rocketLock;

        private new void Awake()
        {
            base.Awake();
            rocketLock = GetComponentInChildren<RocketLockTarget>();
        }

        private new void Start()
        {
            base.Start();
            rocketLock.Owner = owner;
        }

        private new void FixedUpdate()
        {
            TurnTowardTarget();
            SetVelocityForward();
            NormalizeSpeed();
        }

        private void SetVelocityForward()
        {
            var newVelocity = transform.forward;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }

        private void TurnTowardTarget()
        {
            if(rocketLock.ActualTarget != null)
            {
                if(transform.InverseTransformPoint(rocketLock.ActualTarget.transform.position).x < 0) // If the target is on the left we turn to the left
                {
                    transform.Rotate(Vector3.down * TurnSpeed * Time.deltaTime);
                }
                else // If the target is on the right we turn to the right
                {
                    transform.Rotate(Vector3.up * TurnSpeed * Time.deltaTime);
                }
            }
        }

        // We override it beacause we don't need the default behaviour of the trigger
        private new void OnTriggerEnter(Collider other) { }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                DestroyObject();
            }
        }
    }
}