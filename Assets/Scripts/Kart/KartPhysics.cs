using System.Collections;
using UnityEngine;
using HUD;


namespace Kart
{
    /* 
     * Class for handling physics for the kart : 
     * - Forces
     * - Velocity
     * - Drag
     * - Torques
     */
    public class KartPhysics : BaseKartComponent
    {
        [Header("Driving")]
        public float Speed;
        public float MaxMagnitude;

        [Header("Gravity")]
        public float JumpForce;
        public float DoubleJumpUpForce;
        public float DoubleJumpDirectionalForce;
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
        public float TurnSlowValue;
        public float CapSpeedInTurn;

        private KartStates kartStates;

        public float PlayerVelocity;
        public Rigidbody rb;

        private float controlMagnitude;
        private float controlSpeed;
        private float currentTimer;

        private new void Awake()
        {
            controlMagnitude = MaxMagnitude;
            controlSpeed = Speed;
            kartStates = GetComponentInParent<KartStates>();
            rb = GetComponentInParent<Rigidbody>();
            rb.centerOfMass = CenterOfMassOffset;
        }

        private void Update()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            PlayerVelocity = localVelocity.z;

            //if (KartEvents.OnVelocityChange != null)
                //KartEvents.OnVelocityChange(rb.velocity.magnitude);

            //kartSounds.SetMotorPitch(0.5f + 0.5f * (localVelocity.magnitude / MaxMagnitude));
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

        public void TurnUsingTorque(Vector3 direction, float turnAxis)
        {
            TurnSlowDown(turnAxis);
            if (kartStates.AirState != AirStates.InAir)
            {
                rb.AddRelativeTorque(direction * TurnTorqueSpeed, ForceMode.Force);
            }
        }

        public void TurnSlowDown(float turnAxis)
        {
            if (kartStates.TurningState != TurningStates.NotTurning && PlayerVelocity > CapSpeedInTurn)
            {
                float backwardForce = TurnSlowValue * -Mathf.Abs(turnAxis);
                rb.AddForce(transform.forward * backwardForce);
            }
        }

        public void Jump()
        {
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        public void DriftJump()
        {
            rb.AddRelativeForce(Vector3.up * JumpForce / 3f, ForceMode.Impulse);
        }

        public void DoubleJump(Vector3 doubleJumpDirectionVector, float directionalForceMultiplier)
        {
            var forceUp = Vector3.up * DoubleJumpUpForce;
            var forceDirectional = doubleJumpDirectionVector * DoubleJumpDirectionalForce * directionalForceMultiplier;
            rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Impulse);
        }

        public void DriftJump(float value)
        {
            rb.AddRelativeForce(Vector3.up * DriftJumpForce * value, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            rb.AddRelativeForce(Vector3.forward * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            rb.AddRelativeForce(Vector3.back * value * Speed / 2, ForceMode.Force);
        }

        public float SpeedCheck(float ComparValue)
        {
            return PlayerVelocity - ComparValue;
        }

        public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            // clamp pour ne pas qu'avec des drift enchainés rapide le cap s'incrémente
            MaxMagnitude = Mathf.Clamp(MaxMagnitude, 0, controlMagnitude);
            Speed = Mathf.Clamp(Speed, 0, controlSpeed);
            //SpeedCap Increase
            MaxMagnitude += magnitudeBoost;
            Speed += speedBoost;
            // Boost Launch
            float effectDuration = boostDuration;

            currentTimer = 0f;
            while (currentTimer < effectDuration)
            {
                float boost = Mathf.Lerp(5, 0, currentTimer / effectDuration);
                rb.AddRelativeForce(Vector3.forward * boost, ForceMode.VelocityChange);
                currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            currentTimer = 0f;
            while (currentTimer < effectDuration)
            {
                Speed = Mathf.Lerp(controlSpeed + speedBoost, controlSpeed, currentTimer / effectDuration);
                MaxMagnitude = Mathf.Lerp(controlMagnitude + magnitudeBoost, controlMagnitude, currentTimer / effectDuration);
                currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

    }
}