using System;
using System.Collections;
using UnityEngine;

namespace Kart {
    /*
     * States : 
     * - Turning 
     * - Drift 
     * - Air
     * - Acceleration ?
     */
    public enum AccelerationStates { Forward, Back, None }
    public enum TurningStates { NotTurning, Left, Right }
    public enum DriftBoostStates { NotDrifting, SimpleDrift, OrangeDrift, RedDrift, Turbo }
    public enum AirStates { Grounded, InAir }
    public enum CrashedStates { NotCrashed, Crashed }

    public class KartStates : MonoBehaviour{

        public AccelerationStates AccelerationState = AccelerationStates.None;
        public TurningStates TurningState = TurningStates.NotTurning;
        public TurningStates DriftTurnState = TurningStates.NotTurning;
        public DriftBoostStates DriftBoostState = DriftBoostStates.NotDrifting;
        public AirStates AirState = AirStates.Grounded;
        public CrashedStates CrashedState = CrashedStates.NotCrashed;

        public float DistanceForGrounded;
        public float VelocityDetectionThreshold;
        public float CrashDuration;

        private Rigidbody rb;
        private KartEvents kartEvents;

        private void Awake()
        {
            kartEvents = GetComponent<KartEvents>();
            rb = GetComponentInChildren<Rigidbody>();

            kartEvents.OnCollisionEnterGround += () => AirState = AirStates.Grounded;
            kartEvents.OnHit += () => StartCoroutine(CrashBehaviour());
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
                AirState = AirStates.Grounded;
            }
            else
            {
                AirState = AirStates.InAir;
            }
        }

        private void CheckAcceleration()
        {
            if(transform.InverseTransformDirection(rb.velocity).z > VelocityDetectionThreshold)
            {
                AccelerationState = AccelerationStates.Forward;
            }
            else if(transform.InverseTransformDirection(rb.velocity).z < -VelocityDetectionThreshold)
            {
                AccelerationState = AccelerationStates.Back;
            }
            else
            {
                AccelerationState = AccelerationStates.None;
            }
        }

        public bool IsGrounded()
        {
            return AirState == AirStates.Grounded;
        }

        IEnumerator CrashBehaviour()
        {
            CrashedState = CrashedStates.Crashed;
            yield return new WaitForSeconds(CrashDuration);
            CrashedState = CrashedStates.NotCrashed;
            kartEvents.OnHitRecover();
        }
    }
}