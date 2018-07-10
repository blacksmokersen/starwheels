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
            if (kartStates.AirState != AirStates.InAir)
            {
                rb.AddRelativeTorque(direction * TurnTorqueSpeed, ForceMode.Force);
            }
        }

        public void Jump(float percentage = 1f)
        {
            rb.AddRelativeForce(Vector3.up * JumpForce * percentage, ForceMode.Impulse);
        }

        public void DoubleJump(float value, float turnAxis, float accelerateAxis)
        {
            float upAndDownAxis = Input.GetAxis(Constants.UpAndDownAxis);

            if (kartStates.TurningState == TurningStates.NotTurning)
            {
                if (upAndDownAxis >= 0.1f)
                {
                    Debug.Log("JumpBack");
                    karteffects.BackJumpAnimation();
                    rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
                    rb.AddRelativeForce(Vector3.forward * -JumpForce * value*2, ForceMode.Impulse);
                }
                else if (upAndDownAxis <= -0.1f)
                {
                    Debug.Log("Jumpfront");
                    karteffects.FrontJumpAnimation();
                    rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
                    rb.AddRelativeForce(Vector3.forward * JumpForce * value*2, ForceMode.Impulse);
                }
                else
                {
                    Debug.Log("JumpStraight");
                    rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
                }
            }
            else if (kartStates.TurningState == TurningStates.Left)
            {
                Debug.Log("JumpLeft");
                karteffects.LeftJumpAnimation();
                rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
                rb.AddRelativeForce(Vector3.left * JumpForce * value*2, ForceMode.Impulse);
            }
            else if (kartStates.TurningState == TurningStates.Right)
            {

                Debug.Log("JumpRight");
                karteffects.RightJumpAnimation();
                rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
                rb.AddRelativeForce(Vector3.left * -JumpForce * value*2, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("JumpStraight");
                rb.AddRelativeForce(Vector3.up * JumpForce / 5 * value, ForceMode.Impulse);
            }
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
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
            {
                Debug.Log(Speed);
                Speed = Mathf.Lerp(controlSpeed + speedBoost, controlSpeed, t);
                MaxMagnitude = Mathf.Lerp(controlMagnitude + magnitudeBoost, controlMagnitude, t);
                yield return null;
            }
        }
    }
}