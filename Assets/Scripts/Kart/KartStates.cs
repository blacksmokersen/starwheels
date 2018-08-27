using System.Collections;
using UnityEngine;

namespace Kart
{

    public enum AccelerationState { None, Forward, Back }
    public enum TurnState { NotTurning, Left, Right }
    public enum DriftBoostState { NotDrifting, Simple, Orange, Red, Turbo }
    public enum AirState { Ground, Air }
    public enum CrashState { NotCrashed, Crashed }

    public class KartStates : MonoBehaviour
    {

        public AccelerationState AccelerationState = AccelerationState.None;
        public TurnState TurningState = TurnState.NotTurning;
        public TurnState DriftTurnState = TurnState.NotTurning;
        public DriftBoostState DriftBoostState = DriftBoostState.NotDrifting;
        public AirState AirState = AirState.Ground;
        public CrashState CrashedState = CrashState.NotCrashed;

        public float DistanceForGrounded;
        public float VelocityDetectionThreshold;
        public float CrashDuration;

        private Rigidbody _rb;
        private KartEvents _kartEvents;

        private void Awake()
        {
            _kartEvents = GetComponent<KartEvents>();
            _rb = GetComponentInChildren<Rigidbody>();

            _kartEvents.OnCollisionEnterGround += () => AirState = AirState.Ground;
            _kartEvents.OnHit += () => StartCoroutine(CrashBehaviour());
        }

        public void FixedUpdate()
        {
            CheckGrounded();
            CheckAcceleration();
        }

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
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

        public bool IsGrounded()
        {
            return AirState == AirState.Ground;
        }

        IEnumerator CrashBehaviour()
        {
            CrashedState = CrashState.Crashed;
            yield return new WaitForSeconds(CrashDuration);
            CrashedState = CrashState.NotCrashed;
            _kartEvents.OnHitRecover();
        }
    }
}
