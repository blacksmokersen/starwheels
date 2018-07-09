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
        [HideInInspector] public Vector3 CenterOfMassOffset;
        [Range(0, 1)] public float MinDrag;
        [Range(0, 10)] public float MaxDrag;
        [Range(0, 1)] [HideInInspector] public float ForwardDrag;
        [Range(0, 1)] [HideInInspector] public float SideDrag;

        [Header("Drift")]
        public float DriftSideSpeed;
        public float DriftForwardSpeed;


        [Header("Turn")]
        public float TurnTorqueSpeed;
        public float CompensationForce;

        private KartStates kartStates;

        public float PlayerVelocity;
        public Rigidbody rb;

        private void Awake()
        {
            kartStates = GetComponentInChildren<KartStates>();
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = CenterOfMassOffset;
        }

        private void Update()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            PlayerVelocity = localVelocity.z;
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxMagnitude);
            rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            CheckDrag();
        }

        public void CompensateSlip()
        {
            var sideVelocity = new Vector3(transform.InverseTransformDirection(rb.velocity).x, 0, 0);
            rb.AddRelativeForce(-sideVelocity * CompensationForce, ForceMode.Force);
        }

        private void CheckDrag()
        {
            if (kartStates.AirState == AirStates.InAir)
                rb.drag = MinDrag;
            else
                rb.drag = MaxDrag;
        }

        private void CustomDrag()
        {
            var vel = transform.InverseTransformDirection(rb.velocity);
            vel.x *= 1.0f - SideDrag;
            vel.z *= 1.0f - ForwardDrag;
            rb.velocity = transform.TransformDirection(vel);
        }

        public void DriftUsingForce(float forwardRatio, float sideRatio, Vector3 directionSide, Vector3 directionFront)
        {
            rb.AddRelativeForce(forwardRatio * directionFront * DriftForwardSpeed, ForceMode.Force);
            rb.AddRelativeForce(sideRatio * directionSide * DriftSideSpeed, ForceMode.Force);
        }

        public void DriftUsingForce(Vector3 direction)
        {
            rb.AddRelativeForce(direction.z * Vector3.forward * DriftForwardSpeed, ForceMode.Force);
            rb.AddRelativeForce(direction.x * Vector3.left * DriftSideSpeed, ForceMode.Force);
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

        public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            MaxMagnitude += magnitudeBoost;
            Speed += speedBoost;
            yield return new WaitForSeconds(boostDuration);
            Speed -= speedBoost;
            MaxMagnitude -= magnitudeBoost;
        }
    }
}