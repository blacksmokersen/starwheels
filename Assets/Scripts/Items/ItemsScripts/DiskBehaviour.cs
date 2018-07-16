using UnityEngine;
using Kart;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class DiskBehaviour : ItemBehaviour
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
        
        public override void SetOwner(KartInventory kart)
        {
            rb.velocity = kart.transform.forward * Speed;
            transform.position = kart.ItemPositions.FrontPosition.position;
        }

        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.normalized * Speed;
            CheckGrounded();
            AddGravity();
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
