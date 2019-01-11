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
        public float AntiShakingThreshold;

        private float _actualTurnSpeed;
        private RocketLockTarget _rocketLock;

        // CORE

        private new void Awake()
        {
            base.Awake();
            _rocketLock = GetComponentInChildren<RocketLockTarget>();
        }

        private new void FixedUpdate()
        {
            TurnTowardTarget();
            SetVelocityForward();
            base.FixedUpdate();
        }

        // PUBLIC

        // PRIVATE

        private void SetVelocityForward()
        {
            var newVelocity = transform.forward;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }

        private void TurnTowardTarget()
        {
            if (_rocketLock.CurrentTarget != null)
            {
                if (transform.InverseTransformPoint(_rocketLock.CurrentTarget.transform.position).x < -AntiShakingThreshold) // If the target is on the left we turn to the left
                {
                    transform.Rotate(Vector3.down * _actualTurnSpeed * Time.deltaTime);
                }
                else if (transform.InverseTransformPoint(_rocketLock.CurrentTarget.transform.position).x > AntiShakingThreshold)  // If the target is on the right we turn to the right
                {
                    transform.Rotate(Vector3.up * _actualTurnSpeed * Time.deltaTime);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (BoltNetwork.IsServer)
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
                {
                    PlayCollisionSound();
                    DestroyObject();
                }
            }
        }

        public IEnumerator StartQuickTurn()
        {
            _actualTurnSpeed = QuickTurnSpeed;
            yield return new WaitForSeconds(QuickTurnDuration);
            _actualTurnSpeed = NormalTurnSpeed;
        }
    }
}
