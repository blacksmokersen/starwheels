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

        private float actualTurnSpeed;
        private RocketLockTarget rocketLock;

        private float followTargetDelay;
        private bool _followTarget = false;

        private new void Awake()
        {
            base.Awake();
            rocketLock = GetComponentInChildren<RocketLockTarget>();
            followTargetDelay = GetComponentInChildren<RocketLockTarget>().SecondsBeforeSearchingTarget;
        }

        private void Start()
        {
            rocketLock.Owner = owner;
            StartCoroutine(DelayEnableFollowTarget());
        }

        private new void FixedUpdate()
        {
            TurnTowardTarget();
            if(_followTarget)
            SetVelocityForward();
            base.FixedUpdate();
        }

        private void SetVelocityForward()
        {
            var newVelocity = transform.forward;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }

        private void TurnTowardTarget()
        {
            if (rocketLock.ActualTarget != null)
            {
                if (transform.InverseTransformPoint(rocketLock.ActualTarget.transform.position).x < -AntiShakingThreshold) // If the target is on the left we turn to the left
                {
                    transform.Rotate(Vector3.down * actualTurnSpeed * Time.deltaTime);
                }
                else if (transform.InverseTransformPoint(rocketLock.ActualTarget.transform.position).x > AntiShakingThreshold)  // If the target is on the right we turn to the right
                {
                    transform.Rotate(Vector3.up * actualTurnSpeed * Time.deltaTime);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.Layer.Ground))
            {
                PlayCollisionSound();
                DestroyObject();
            }
        }

        public IEnumerator StartQuickTurn()
        {
            actualTurnSpeed = QuickTurnSpeed;
            yield return new WaitForSeconds(QuickTurnDuration);
            actualTurnSpeed = NormalTurnSpeed;
        }
        public IEnumerator DelayEnableFollowTarget()
        {
            yield return new WaitForSeconds(followTargetDelay);
            _followTarget = true;
        }
    }
}
