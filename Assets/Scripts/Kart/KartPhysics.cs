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
        public float DriftJumpForce;
        public float GravityForce;
        [HideInInspector] public Vector3 CenterOfMassOffset;
        [Range(0, 1)] public float MinDrag;
        [Range(0, 10)] public float MaxDrag;
        [Range(0, 1)] [HideInInspector] public float ForwardDrag;
        [Range(0, 1)] [HideInInspector] public float SideDrag;

        [Header("Drift")]
        public float DriftGlideOrientation = 500f;
        public float DriftGlideBack = 500f;
        [Range(0, 2)] public float DriftBoostImpulse = 0.5f;


        [Header("Turn")]
        public float TurnTorqueSpeed;
        public float CompensationForce;

        private KartStates kartStates;
        private KartEffects karteffects;

        public float PlayerVelocity;
        public Rigidbody rb;

        private float controlMagnitude;
        private float controlSpeed;

        private void Awake()
        {
            controlMagnitude = MaxMagnitude;
            controlSpeed = Speed;
            kartStates = GetComponentInChildren<KartStates>();
            karteffects = GetComponentInChildren<KartEffects>();
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

        public void DriftUsingForce()
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                rb.AddRelativeForce(Vector3.right * DriftGlideOrientation, ForceMode.Force);
                rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                rb.AddRelativeForce(Vector3.left * DriftGlideOrientation, ForceMode.Force);
                rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
            }
        }

        public void TurnUsingTorque(Vector3 direction)
        {
            if (kartStates.AirState != AirStates.InAir)
            {
                rb.AddRelativeTorque(direction * TurnTorqueSpeed, ForceMode.Force);
            }
        }

        public void Jump(float percentage = 1f)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
        }

        public void LeftJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.left * JumpForce * value * 2, ForceMode.Impulse);
        }

        public void RightJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.left * -JumpForce * value * 2, ForceMode.Impulse);
        }

        public void FrontJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.forward * JumpForce / 2 * value * 2, ForceMode.Impulse);
        }

        public void BackJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
            rb.AddRelativeForce(Vector3.forward * -JumpForce / 2 * value * 2, ForceMode.Impulse);
        }

        public void StraightJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 4 * value, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            rb.AddRelativeForce(Vector3.forward * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            rb.AddRelativeForce(Vector3.back * value * Speed, ForceMode.Force);
        }

        public float SpeedCheck(float ComparValue)
        {
            return PlayerVelocity - ComparValue;
        }

        public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            //SpeedCap Increase
            MaxMagnitude += magnitudeBoost;
            Speed += speedBoost;
            // Boost Launch
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
            {
                float boost = Mathf.Lerp(1, 0, t);
                rb.AddRelativeForce(Vector3.forward * boost, ForceMode.VelocityChange);
                yield return null;
            }
            yield return new WaitForSeconds(boostDuration);
            //SpeedCap Decrease
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
            {
                Speed = Mathf.Lerp(controlSpeed + speedBoost, controlSpeed, t);
                MaxMagnitude = Mathf.Lerp(controlMagnitude + magnitudeBoost, controlMagnitude, t);
                yield return null;
            }
        }
    }
}