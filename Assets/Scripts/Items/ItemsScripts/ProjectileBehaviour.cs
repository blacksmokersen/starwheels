using UnityEngine;
using Kart;

namespace Items
{
    public class ProjectileBehaviour : ItemBehaviour
    {
        [Header("Projectile parameters")]
        public float Speed;

        [Header("Ground parameters")]
        public AirStates AirState = AirStates.InAir;
        public float DistanceForGrounded;
        public float LocalGravity;

        private Rigidbody rb;

        private void Update()
        {
            CheckGrounded();
        }

        private void FixedUpdate()
        {
            rb.velocity = rb.velocity.normalized * Speed;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public override void SetOwner(KartInventory kart)
        {
            rb.velocity = kart.transform.forward * Speed;
            transform.position = kart.ItemPositions.FrontPosition.position;
        }

        private void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, DistanceForGrounded, 1 << LayerMask.NameToLayer(Constants.GroundLayer)))
            {
                rb.useGravity = false;

                var velocity = rb.velocity;
                velocity.y = 0;
                rb.velocity = velocity;

                var position = transform.position;
                position.y = hit.point.y + DistanceForGrounded - 0.1f;
                transform.position = position;
            }
            else
            {
                rb.useGravity = true;
                ApplyLocalGravity();
            }
        }

        public void ApplyLocalGravity()
        {
            rb.AddForce(Vector3.down * LocalGravity);
        }

        protected void DestroyObject()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
