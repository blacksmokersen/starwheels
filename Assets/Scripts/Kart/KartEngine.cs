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
    [RequireComponent(typeof(Rigidbody))]
    public class KartEngine : BaseKartComponent
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
        [Range(0, 1)] public float MinDrag;
        [Range(0, 10)] public float MaxDrag;

        [Header("Drift")]
        public float DriftGlideOrientation = 500f;
        public float DriftGlideBack = 500f;
        [Range(0, 2)] public float DriftBoostImpulse = 0.5f;
        public float DriftTurnSpeed = 150;
        public float MaxExteriorAngle = 0.05f;
        public float BoostPowerImpulse;

        [Header("Turn")]
        public float TurnTorqueSpeed;
        public float CompensationForce;
        public float TurnSlowValue;
        public float CapSpeedInTurn;
        [Range(1, 3)] public float LowerTurnSensitivity;

        [Header("Stabilization")]
        public float RotationStabilizationSpeed;

        public bool Crash;

        private KartStates kartStates;
       // private KartSoundsScript kartSounds;

        public float PlayerVelocity;
        public Rigidbody rb;

        private float controlMagnitude;
        private float controlSpeed;
        private float currentTimer;

        private void Awake()
        {
            controlMagnitude = MaxMagnitude;
            controlSpeed = Speed;
            kartStates = GetComponentInChildren<KartStates>();
          //  kartSounds = GetComponentInChildren<KartSoundsScript>();
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            PlayerVelocity = localVelocity.z;

            if (KartEvents.OnAccelerate != null)
                KartEvents.OnAccelerate(rb.velocity.magnitude);

            kartSounds.SetMotorPitch(0.5f + 0.5f * (localVelocity.magnitude / MaxMagnitude));
        }

        private void FixedUpdate()
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxMagnitude);
            rb.AddForce(Vector3.down * GravityForce, ForceMode.Acceleration);
            CheckDrag();
            StabilizeRotation();
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


        public void Jump(float percentage = 1f)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
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

        public void DriftTurn(float angle)
        {
            if (kartStates.DriftTurnState == DriftTurnStates.DriftingLeft)
            {
                if (angle != 0)
                    angle = Mathf.Clamp(angle, -0.8f, -MaxExteriorAngle);
                else
                    angle = Mathf.Clamp(angle, -0.8f, -0.2f);
            }
            else if (kartStates.DriftTurnState == DriftTurnStates.DriftingRight)
            {
                if (angle != 0)
                    angle = Mathf.Clamp(angle, MaxExteriorAngle, 0.8f);
                else
                    angle = Mathf.Clamp(angle, 0.2f, 0.8f);
            }
            transform.Rotate(Vector3.up * angle * DriftTurnSpeed * Time.deltaTime);
        }

        public void StabilizeRotation()
        {
            if (kartStates.AirState == AirStates.InAir)
            {
                var actualRotation = transform.localRotation;
                actualRotation.x = Mathf.Lerp(actualRotation.x, 0, RotationStabilizationSpeed);
                actualRotation.z = Mathf.Lerp(actualRotation.z, 0, RotationStabilizationSpeed);
                transform.localRotation = actualRotation;
            }
        }

        public void LooseHealth(float crashTimer)
        {
            StartCoroutine(CrashBehaviour(crashTimer));
        }

        IEnumerator CrashBehaviour(float crashTimer)
        {
            Crash = true;
            yield return new WaitForSeconds(crashTimer);
            Crash = false;
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