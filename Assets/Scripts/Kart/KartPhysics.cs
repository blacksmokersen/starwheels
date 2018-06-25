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
        public float Speed;
        public float JumpForce;
        public float DriftSideSpeed;
        public float DriftForwardSpeed;
        public float BoostSpeed;

        private KartStates kartStates;
        private Rigidbody rb;

        private void Awake()
        {
            kartStates = GetComponentInParent<KartStates>();
            rb = GetComponent<Rigidbody>();
        }

        public void Drift(Vector3 directionSide, Vector3 directionFront)
        {
            rb.AddRelativeForce(directionSide * DriftSideSpeed, ForceMode.Force);
            rb.AddRelativeForce(directionFront * DriftForwardSpeed, ForceMode.Force);
        }

        public void Jump(float percentage = 1f)
        {
            rb.AddRelativeForce(Vector3.forward * JumpForce * percentage, ForceMode.Impulse);
        }

        public void Accelerate(float value)
        {
            rb.AddRelativeForce(Vector3.up * value * Speed, ForceMode.Force);
        }

        public void Decelerate(float value)
        {
            rb.AddRelativeForce(Vector3.down * value * Speed, ForceMode.Force);
        }

        public IEnumerator Boost()
        {
            Speed += BoostSpeed;
            yield return new WaitForSeconds(2f);
            Speed -= BoostSpeed;
        }
    }
}