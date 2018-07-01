using System.Collections;
using UnityEngine;


namespace Kart
{
    /* 
     * Class for handling physics for the kart : 
     * - Forces
     * - Velocity
     */
    [RequireComponent(typeof(Rigidbody))]
    public class KartPhysics : MonoBehaviour
    {
        [Header("Driving")]
        public float Speed;
        public float MaxMagnitude;
        public float JumpForce;
        public Vector3 CenterOfMassOffset;


        [Header("Drift")]
        public float DriftSideSpeed;
        public float DriftForwardSpeed;
        public float DriftTorqueSpeed;
        public float BoostSpeed;

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
        }

        public void CompensateSlip()
        {
            var sideVelocity = new Vector3(transform.InverseTransformDirection(rb.velocity).x,0,0);            
            rb.AddRelativeForce(-sideVelocity * CompensationForce, ForceMode.Force);
        }

        public void DriftUsingForce(Vector3 directionSide, Vector3 directionFront)
        {
            rb.AddRelativeForce(directionSide * DriftSideSpeed, ForceMode.Force);
            rb.AddRelativeForce(directionFront * DriftForwardSpeed, ForceMode.Force);
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

        public IEnumerator Boost()
        {
            MaxMagnitude += 10f;
            Speed += BoostSpeed;
            yield return new WaitForSeconds(2f);
            Speed -= BoostSpeed;
            MaxMagnitude -= 10f;
        }
    }
}