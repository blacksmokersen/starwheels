using System.Collections;
using UnityEngine;

namespace Kart
{
    public enum AccelerationState { None, Forward, Back }
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftBoostState { NotDrifting, Simple, Orange, Red, Turbo }

    public class KartStates : MonoBehaviour
    {
        public AccelerationState AccelerationState = AccelerationState.None;
        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftBoostState DriftBoostState = DriftBoostState.NotDrifting;

        private bool _isOnGround = true;
        private bool _isCrashed = false;

        public float DistanceForGrounded;
        public float VelocityDetectionThreshold;
        public float CrashDuration;

        private Rigidbody _rb;
        private KartEvents _kartEvents;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInChildren<Rigidbody>();
            _kartEvents = GetComponent<KartEvents>();

            _kartEvents.OnCollisionEnterGround += () => _isOnGround = true;
            _kartEvents.OnHit += () => StartCoroutine(CrashBehaviour());
        }

        private void FixedUpdate()
        {
            CheckGrounded();
            CheckAcceleration();
        }

        // PUBLIC

        public void SetTurnState(TurnState state)
        {
            TurningState = state;
        }

        public void SetTurnState(float turnValue)
        {
            if (turnValue > 0)
            {
                SetTurnState(TurnState.Right);
            }
            else if (turnValue < 0)
            {
                SetTurnState(TurnState.Left);
            }
            else
            {
                SetTurnState(TurnState.NotTurning);
            }
        }

        public void SetDriftTurnState(TurnState state)
        {
            DriftTurnState = state;
        }

        public void SetDriftBoostState(DriftBoostState state)
        {
            DriftBoostState = state;
        }

        public bool IsGrounded()
        {
            return _isOnGround;
        }

        public bool IsCrashed()
        {
            return _isCrashed;
        }

        public bool IsDriftTurning()
        {
            return DriftTurnState == TurnState.NotTurning;
        }

        public bool IsDriftSideEqualsTurnSide()
        {
            return TurningState == DriftTurnState;
        }

        public bool IsDriftSideDifferentFromTurnSide()
        {
            if (TurningState == TurnState.NotTurning || DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return !IsDriftSideEqualsTurnSide();
        }

        // PRIVATE

        private void CheckGrounded()
        {
            _isOnGround = Physics.Raycast(
                transform.position,
                transform.TransformDirection(Vector3.down),
                DistanceForGrounded,
                1 << LayerMask.NameToLayer(Constants.GroundLayer)
            );
        }

        private void CheckAcceleration()
        {
            if (transform.InverseTransformDirection(_rb.velocity).z > VelocityDetectionThreshold)
            {
                AccelerationState = AccelerationState.Forward;
            }
            else if (transform.InverseTransformDirection(_rb.velocity).z < -VelocityDetectionThreshold)
            {
                AccelerationState = AccelerationState.Back;
            }
            else
            {
                AccelerationState = AccelerationState.None;
            }
        }

        private IEnumerator CrashBehaviour()
        {
            _isCrashed = true;
            yield return new WaitForSeconds(CrashDuration);
            _isCrashed = false;
            _kartEvents.OnHitRecover();
        }
    }
}
