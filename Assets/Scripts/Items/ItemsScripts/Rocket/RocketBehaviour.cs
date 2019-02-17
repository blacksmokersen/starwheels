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
        public float SecondsBeforeIgnoringTarget;

        private float _actualTurnSpeed;
        private bool _ignoreTarget = false;
        private RocketLockTarget _rocketLock;

        // MONO

        private new void Awake()
        {
            base.Awake();
            _rocketLock = GetComponentInChildren<RocketLockTarget>();
        }

        private void Start()
        {
            StartCoroutine(IgnoreTargetAfterXSeconds(SecondsBeforeIgnoringTarget));
        }

        private new void FixedUpdate()
        {
            TurnTowardTarget();
            SetVelocityForward();
            base.FixedUpdate();
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

        // PUBLIC

        public IEnumerator StartQuickTurn()
        {
            _actualTurnSpeed = QuickTurnSpeed;
            yield return new WaitForSeconds(QuickTurnDuration);
            _actualTurnSpeed = NormalTurnSpeed;
        }
        // PRIVATE

        private void SetVelocityForward()
        {
            var newVelocity = transform.forward;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }

        private void TurnTowardTarget()
        {
            if (_rocketLock.CurrentTarget != null && _ignoreTarget == false)
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

        private IEnumerator IgnoreTargetAfterXSeconds(float x)
        {
            yield return new WaitForSeconds(x);
            _ignoreTarget = true;
        }
    }
}
