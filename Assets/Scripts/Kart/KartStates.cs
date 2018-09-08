using System.Collections;
using UnityEngine;

namespace Kart
{
    public enum AirState { Ground, Air }
    public enum AccelerationState { None, Forward, Back }
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftState { NotDrifting, White, Orange, Red, Turbo }

    public class KartStates : MonoBehaviour
    {
        public AirState AirState = AirState.Ground;
        public AccelerationState AccelerationState = AccelerationState.None;
        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftState DriftState = DriftState.NotDrifting;

        public float DistanceForGrounded;
        public float VelocityDetectionThreshold;
        public float CrashDuration;

        private bool _isCrashed = false;

        private Rigidbody _rb;
        private KartEvents _kartEvents;

        // CORE

        private void Awake()
        {
            _rb = GetComponentInChildren<Rigidbody>();
            _kartEvents = GetComponent<KartEvents>();

            _kartEvents.OnDriftLeft += () => DriftTurnState = TurnState.Left;
            _kartEvents.OnDriftRight += () => DriftTurnState = TurnState.Right;

            _kartEvents.OnDriftWhite += () => DriftState = DriftState.White;
            _kartEvents.OnDriftOrange += () => DriftState = DriftState.Orange;
            _kartEvents.OnDriftRed += () => DriftState = DriftState.Red;

            _kartEvents.OnDriftBoostStart += () => DriftTurnState = TurnState.NotTurning;
            _kartEvents.OnDriftBoostStart += () => DriftState = DriftState.Turbo;

            _kartEvents.OnDriftReset += () => DriftTurnState = TurnState.NotTurning;
            _kartEvents.OnDriftReset += () => DriftState = DriftState.NotDrifting;

            _kartEvents.OnCollisionEnterGround += () => AirState = AirState.Ground;
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

        public bool IsGrounded()
        {
            return AirState == AirState.Ground;
        }

        public bool IsDrifting()
        {
            return DriftTurnState != TurnState.NotTurning;
        }

        public bool IsCrashed()
        {
            return _isCrashed;
        }

        public bool IsDriftSideEqualsTurnSide()
        {
            if (TurningState == TurnState.NotTurning || DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return TurningState == DriftTurnState;
        }

        public bool IsDriftSideDifferentFromTurnSide()
        {
            if (TurningState == TurnState.NotTurning || DriftTurnState == TurnState.NotTurning)
            {
                return false;
            }

            return TurningState != DriftTurnState;
        }

        // PRIVATE

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.Layer.Ground)))
            {
                AirState = AirState.Ground;
            }
            else
            {
                AirState = AirState.Air;
            }
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
