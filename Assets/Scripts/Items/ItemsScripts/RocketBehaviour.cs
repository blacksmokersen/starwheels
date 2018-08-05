using System.Collections;
using UnityEngine;

namespace Items
{
    public class RocketBehaviour : ProjectileBehaviour
    {
        [Header("Rocket parameters")]
        public float MaxAngle;
        public float NormalTurnSpeed;
        public float QuickTurnSpeed;
        public float QuickTurnDuration;

        private float actualTurnSpeed;
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
                    transform.Rotate(Vector3.down * actualTurnSpeed * Time.deltaTime);
                }
                else // If the target is on the right we turn to the right
                {
                    transform.Rotate(Vector3.up * actualTurnSpeed * Time.deltaTime);
                }
            }
        }

        // We override it because we don't need the default behaviour of the trigger
        private new void OnTriggerEnter(Collider other) { }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer(Constants.GroundLayer))
            {
                DestroyObject();
            }
        }

        public IEnumerator StartQuickTurn()
        {
            actualTurnSpeed = QuickTurnSpeed;
            yield return new WaitForSeconds(QuickTurnDuration);
            actualTurnSpeed = NormalTurnSpeed;
        }
    }
}