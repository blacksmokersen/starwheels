using System;
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
    public enum DriftTurnStates { NotDrifting, DriftingLeft, DriftingRight }
    public enum DriftBoostStates { NotDrifting, SimpleDrift, OrangeDrift, RedDrift, Turbo }
    public enum AirStates { Grounded, InAir }

    public class KartStates : MonoBehaviour{

        public AccelerationStates AccelerationState = AccelerationStates.None;
        public TurningStates TurningState = TurningStates.NotTurning;
        public DriftTurnStates DriftTurnState = DriftTurnStates.NotDrifting;
        public DriftBoostStates DriftBoostState = DriftBoostStates.NotDrifting;
        public AirStates AirState = AirStates.Grounded;

        public float DistanceForGrounded;
        public float VelocityDetectionThreshold;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody>();
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
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * DistanceForGrounded, Color.yellow);
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
    }
}