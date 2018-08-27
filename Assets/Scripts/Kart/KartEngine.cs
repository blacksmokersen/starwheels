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

        private Rigidbody _rb;
        private float _controlMagnitude;
        private float _controlSpeed;
        private float _currentTimer;

        // CORE

        private new void Awake()
        {
            base.Awake();

            _controlMagnitude = MaxMagnitude;
            _controlSpeed = Speed;
            _rb = GetComponentInParent<Rigidbody>();
            _rb.centerOfMass = CenterOfMassOffset;
        }

        private void Update()
        {
            PlayerVelocity = transform.InverseTransformDirection(_rb.velocity).z;
            kartEvents.OnVelocityChange(_rb.velocity);
        }

        private void FixedUpdate()
        {
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, MaxMagnitude);
            _rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);

            CheckDrag();
            StabilizeRotation();
            CompensateSlip();
        }

        // PUBLIC

        public void CompensateSlip()
        {
            var sideVelocity = new Vector3(transform.InverseTransformDirection(_rb.velocity).x, 0, 0);
            _rb.AddRelativeForce(-sideVelocity * SlipCompensationForce, ForceMode.Force);
        }

        public void DriftUsingForce()
        {
            if (kartStates.DriftTurnState == TurnState.Left)
            {
                _rb.AddRelativeForce(Vector3.right * DriftGlideOrientation, ForceMode.Force);
                _rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
            }
            else if (kartStates.DriftTurnState == TurnState.Right)
            {
                _rb.AddRelativeForce(Vector3.left * DriftGlideOrientation, ForceMode.Force);
                _rb.AddRelativeForce(Vector3.back * DriftGlideBack, ForceMode.Force);
            }
        }

        public void TurnUsingTorque(Vector3 direction, float turnAxis)
        {
            TurnSlowDown(turnAxis);
            if (kartStates.IsGrounded())
            {
                _rb.AddRelativeTorque(direction * TurnTorqueSpeed, ForceMode.Force);
            }
        }

        public void TurnSlowDown(float turnAxis)
        {
            if (kartStates.TurningState != TurnState.NotTurning && PlayerVelocity > CapSpeedInTurn)
            {
                float backwardForce = TurnSlowValue * -Mathf.Abs(turnAxis);
                _rb.AddForce(transform.forward * backwardForce);
            }
        }

        public void Jump(float percentage = 1f)
        {
            kartEvents.OnJump();
            _rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
        }

        public void DoubleJump(Vector3 doubleJumpDirectionVector, float directionalForceMultiplier)
        {
            var forceUp = Vector3.up * DoubleJumpUpForce;
            var forceDirectional = doubleJumpDirectionVector * DoubleJumpDirectionalForce * directionalForceMultiplier;
            _rb.AddRelativeForce(forceUp + forceDirectional, ForceMode.Impulse);
        }

        public void DriftJump()
        {
            _rb.AddRelativeForce(Vector3.up * DriftJumpForce, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            _rb.AddRelativeForce(Vector3.forward * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            _rb.AddRelativeForce(Vector3.back * value * Speed / 2, ForceMode.Force);
        }

        public void DriftTurn(float angle)
        {
            float angleRestrain = angle;

            if (kartStates.DriftTurnState == TurnState.Left)
            {
                angleRestrain = angle <= -JoystickDeadZone2 ? MaxInteriorAngle : angle >= JoystickDeadZone1 ? MaxExteriorAngle : 100;
                angle = angle <= -JoystickDeadZone2 ? angle : angle >= JoystickDeadZone1 ? angle : 1;
                _rb.AddTorque(Vector3.up * (-angleRestrain * Mathf.Abs(angle)) * DriftTurnSpeed * Time.deltaTime);
            }
            else if (kartStates.DriftTurnState == TurnState.Right)
            {
                angleRestrain = angle <= -JoystickDeadZone2 ? MaxExteriorAngle : angle >= JoystickDeadZone1 ? MaxInteriorAngle : 100;
                angle = angle <= -JoystickDeadZone2 ? angle : angle >= JoystickDeadZone1 ? angle : 1;
                _rb.AddTorque(Vector3.up * (angleRestrain * Mathf.Abs(angle)) * DriftTurnSpeed * Time.deltaTime);
            }
        }

        public void StabilizeRotation()
        {
            if (kartStates.IsGrounded()) return;

            var actualRotation = transform.parent.localRotation;
            actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
            actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
            transform.parent.localRotation = actualRotation;
        }

        public IEnumerator Boost(float boostDuration, float magnitudeBoost, float speedBoost)
        {
            MaxMagnitude = Mathf.Clamp(MaxMagnitude, 0, _controlMagnitude) + magnitudeBoost;
            Speed = Mathf.Clamp(Speed, 0, _controlSpeed) + speedBoost;

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                _rb.AddRelativeForce(Vector3.forward * BoostPowerImpulse, ForceMode.VelocityChange);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _currentTimer = 0f;
            while (_currentTimer < boostDuration)
            {
                MaxMagnitude = Mathf.Lerp(_controlMagnitude + magnitudeBoost, _controlMagnitude, _currentTimer / boostDuration);
                Speed = Mathf.Lerp(_controlSpeed + speedBoost, _controlSpeed, _currentTimer / boostDuration);
                _currentTimer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        // PRIVATE

        private void CheckDrag()
        {
            _rb.drag = kartStates.IsGrounded() ? MaxDrag : MinDrag;
        }
    }
}
