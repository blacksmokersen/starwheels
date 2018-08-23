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
    public class KartEngine : BaseKartComponent
    {
        [Header("Driving")]
        public float Speed;
        public float MaxMagnitude;
        public float PlayerVelocity;

        [Header("Gravity")]
        public float JumpForce;
        public float DoubleJumpUpForce;
        public float DoubleJumpDirectionalForce;
        public float DriftJumpForce;
        public float GravityForce;
        public Vector3 CenterOfMassOffset;
        [Range(0, 1)] public float MinDrag;
        [Range(0, 10)] public float MaxDrag;


        [Header("Drift")]
        public float DriftGlideOrientation = 500f;
        public float DriftGlideBack = 500f;
        public float DriftTurnSpeed = 150f;
        public float MaxInteriorAngle = 400f;
        public float MaxExteriorAngle = 40f;
        [Range(0, 2)] public float BoostPowerImpulse;
        public const float JoystickDeadZone1 = 0.1f;
        public const float JoystickDeadZone2 = 0.2f;

        [Header("Turn")]
        public float TurnTorqueSpeed;
        public float SlipCompensationForce;
        public float TurnSlowValue;
        public float CapSpeedInTurn;
        [Range(1, 3)] public float LowerTurnSensitivity;

        [Header("Stabilization")]
        public float RotationStabilizationSpeed;

        private Rigidbody rb;
        private float controlMagnitude;
        private float controlSpeed;
        private float currentTimer;

        private new void Awake()
        {
            base.Awake();
            controlMagnitude = MaxMagnitude;
            controlSpeed = Speed;
            kartStates = GetComponentInParent<KartStates>();
            rb = GetComponentInParent<Rigidbody>();
            rb.centerOfMass = CenterOfMassOffset;

        }

        private void Update()
        {
            var localVelocity = transform.InverseTransformDirection(rb.velocity);
            PlayerVelocity = localVelocity.z;
            kartEvents.OnVelocityChange(rb.velocity);
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxMagnitude);
            rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            CheckDrag();
            StabilizeRotation();
            CompensateSlip();
        }

        public void CompensateSlip()
        {
            var sideVelocity = new Vector3(transform.InverseTransformDirection(rb.velocity).x, 0, 0);
            rb.AddRelativeForce(-sideVelocity * SlipCompensationForce, ForceMode.Force);
        }

        private void CheckDrag()
        {
            if (kartStates.AirState == AirStates.InAir)
                rb.drag = MinDrag;
            else
                rb.drag = MaxDrag;
        }

        public void DriftUsingForce()
        {
            if (kartStates.DriftTurnState == TurningStates.Left)
            {
                rb.AddRelativeForce(Vector3.right * DriftGlideOrientation, ForceMode.Force);
                rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
            }
            else if (kartStates.DriftTurnState == TurningStates.Right)
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

        public void Jump(float percentage = 1f)
        {
            kartEvents.OnJump();
            rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
        }

        public void DoubleJump(Vector3 doubleJumpDirectionVector, float directionalForceMultiplier)
        {
            var forceUp = Vector3.up * DoubleJumpUpForce;
            var forceDirectional = doubleJumpDirectionVector * DoubleJumpDirectionalForce * directionalForceMultiplier;
            rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Impulse);
        }

        public void DriftJump()
        {
            rb.AddRelativeForce(Vector3.up * DriftJumpForce, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            rb.AddRelativeForce(Vector3.forward * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            rb.AddRelativeForce(Vector3.back * value * Speed / 2, ForceMode.Force);
        }

        public void DriftTurn(float angle)
        {
            float angleRestrain = angle;
            if (kartStates.DriftTurnState == TurningStates.Left)
            {
                angleRestrain = angle <= -JoystickDeadZone2 ? MaxInteriorAngle : angle >= JoystickDeadZone1 ? MaxExteriorAngle : 100;
                angle = angle <= -JoystickDeadZone2 ? angle : angle >= JoystickDeadZone1 ? angle : 1;
                rb.AddTorque(Vector3.up * (-angleRestrain * Mathf.Abs(angle)) * DriftTurnSpeed * Time.deltaTime);
            }
            else if (kartStates.DriftTurnState == TurningStates.Right)
            {
                angleRestrain = angle <= -JoystickDeadZone2 ? MaxExteriorAngle : angle >= JoystickDeadZone1 ? MaxInteriorAngle : 100;
                angle = angle <= -JoystickDeadZone2 ? angle : angle >= JoystickDeadZone1 ? angle : 1;
                rb.AddTorque(Vector3.up * (angleRestrain * Mathf.Abs(angle)) * DriftTurnSpeed * Time.deltaTime);
            }
        }

        public void StabilizeRotation()
        {
            if (kartStates.AirState == AirStates.InAir)
            {
                var actualRotation = transform.parent.localRotation;
                actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
                actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
                transform.parent.localRotation = actualRotation;
            }
        }

        public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            MaxMagnitude = Mathf.Clamp(MaxMagnitude, 0, controlMagnitude);
            Speed = Mathf.Clamp(Speed, 0, controlSpeed);
            MaxMagnitude += magnitudeBoost;
            Speed += speedBoost;

            currentTimer = 0f;
            while (currentTimer < boostDuration)
            {
                rb.AddRelativeForce(Vector3.forward * BoostPowerImpulse, ForceMode.VelocityChange);
                currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            currentTimer = 0f;
            while (currentTimer < boostDuration)
            {
                Speed = Mathf.Lerp(controlSpeed + speedBoost, controlSpeed, currentTimer / boostDuration);
                MaxMagnitude = Mathf.Lerp(controlMagnitude + magnitudeBoost, controlMagnitude, currentTimer / boostDuration);
                currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}