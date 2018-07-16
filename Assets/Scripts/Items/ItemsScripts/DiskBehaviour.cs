using UnityEngine;
using Kart;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : MonoBehaviour
    {

        [Header("Disk parameters")]
        public float Speed;
        public int ReboundsBeforeEnd;

        [Header("Ground parameters")]
        public AirStates AirState;
        public float DistanceForGrounded;
        public float GravityForce;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.normalized * Speed;
            CheckGrounded();
            AddGravity();
        }

        public void SetDirection(Vector3 direction)
        {
            rb.velocity = direction * Speed;
        }

        private void CheckGrounded()
        {
            if (Physics.Raycast(transform.position, Vector3.down, DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
            {
                var v = rb.velocity;
                v.y = 0;
                rb.velocity = v;
                AirState = AirStates.Grounded;
            }
            else
            {
                AirState = AirStates.InAir;
            }
        }

        private void AddGravity()
        {
            rb.AddForce(Vector3.down * GravityForce);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Constants.KartTag)
            {
                Destroy(gameObject);
            }
            else
            {
                ReboundsBeforeEnd--;
                if (ReboundsBeforeEnd <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
