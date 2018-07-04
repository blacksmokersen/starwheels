using System.Collections;
using UnityEngine;


namespace Kart
{
    /* 
     * Class for handling physics for the kart : 
     * - Forces
     * - Velocity
     * - Drag
     * - Torques
     */
    [RequireComponent(typeof(Rigidbody))]
    public class KartPhysics : MonoBehaviour
    {

        [Header("Driving")]
        public float Speed;
        public float MaxMagnitude;

        [Header("Gravity")]
        public float JumpForce;
        public float GravityForce;
        public Vector3 CenterOfMassOffset;
        [Range(0, 1)] public float MinDrag;
        [Range(0, 10)] public float MaxDrag;
        [Range(0, 1)] public float ForwardDrag;
        [Range(0, 1)] public float SideDrag;

        [Header("Drift")]
        public float DriftSideSpeed;
        public float DriftForwardSpeed;
        public float BoostSpeed;
        [Range(0, 90)] public float ForwardMaxAngle;
        [Range(0, -90)] public float BackMaxAngle;

        [Header("Turn")]
        public float TurnTorqueSpeed;
        public float CompensationForce;

        private KartStates kartStates;
        private Rigidbody rb;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = CenterOfMassOffset;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(CenterOfMassOffset, 0.5f);
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxMagnitude);
            rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            CheckDrag();
        }

        public void CompensateSlip()
        {
            var sideVelocity = new Vector3(transform.InverseTransformDirection(rb.velocity).x,0,0);            
            rb.AddRelativeForce(-sideVelocity * CompensationForce, ForceMode.Force);
        }

        private void CheckDrag()
        {
            if (kartStates.AirState == AirStates.InAir)
            {
                rb.drag = MinDrag;
            }
            else
            {
                rb.drag = MaxDrag;
            }
        }

        private void CustomDrag()
        {
            var vel = transform.InverseTransformDirection(rb.velocity);
            vel.x *= 1.0f - SideDrag; // reduce x component...
            vel.z *= 1.0f - ForwardDrag; // and z component each cycle
            rb.velocity = transform.TransformDirection(vel);
        }

        public void DriftUsingForce(float forwardRatio, float sideRatio, Vector3 directionSide, Vector3 directionFront)
        {
            rb.AddRelativeForce(forwardRatio * directionFront * DriftForwardSpeed, ForceMode.Force);
            rb.AddRelativeForce(sideRatio * directionSide * DriftSideSpeed, ForceMode.Force);
        }

        public float RemapValue(float actualMin, float actualMax, float targetMin, float targetMax, float val)
        {
            return targetMin + (targetMax - targetMin) * ((val - actualMin) / (actualMax - actualMin));
        }

        public void DriftUsingForce(float turnValue)
        {
            float angle = 0f;
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                angle = Mathf.PI - Mathf.Deg2Rad * RemapValue(-1, 1, ForwardMaxAngle, BackMaxAngle, turnValue);
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                angle = Mathf.Deg2Rad * RemapValue(-1, 1, BackMaxAngle, ForwardMaxAngle, turnValue);
            }
            var direction = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
            rb.AddRelativeForce(direction.z * Vector3.forward * DriftForwardSpeed, ForceMode.Force);
            Debug.Log("Forward force : " + (direction.z * Vector3.forward * DriftForwardSpeed));
            rb.AddRelativeForce(direction.x * Vector3.left * DriftSideSpeed, ForceMode.Force);
            Debug.Log("Side force : " + (direction.x * Vector3.left * DriftSideSpeed));
        }

        public void TurnUsingTorque(Vector3 direction)
        {
            rb.AddRelativeTorque(direction * TurnTorqueSpeed, ForceMode.Force);
        }

        public void Jump(float percentage = 1f)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            rb.AddRelativeForce(Vector3.forward * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            rb.AddRelativeForce(Vector3.back * value * Speed, ForceMode.Force);
        }

        public IEnumerator Boost(float boostDuration)
        {
            MaxMagnitude += 10f;
            Speed += BoostSpeed;
            yield return new WaitForSeconds(boostDuration);
            Speed -= BoostSpeed;
            MaxMagnitude -= 10f;
        }
    }
}